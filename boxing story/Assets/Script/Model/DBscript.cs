using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text;



public class DBscript {

    IDbConnection dbconn;
    public delegate void resultDelegate(IDataReader reader);
    public delegate void noResultDelegate();
    void CopyDB()
    {
        
    }
    public void open_db(string str)
    {
        string filepath = string.Empty;
        string connection = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/boxDB.db";
            if (!File.Exists(filepath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/boxDB.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }

            connection = "URI=file:" + Application.persistentDataPath + "/boxDB.db";
        }
        else
        {
            filepath = Application.dataPath + "/boxDB.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/boxDB.db", filepath);
            }
            connection = "URI=file:" + Application.dataPath + "/boxDB.db";
        }

        dbconn = (IDbConnection)new SqliteConnection(connection);

        dbconn.Open();
    }

    public void SelectQuery(String query, resultDelegate resultFunc)
    {
        open_db("boxDB.db");
        IDbCommand dbcmd = dbconn.CreateCommand();

        dbcmd.CommandText = query;


        IDataReader reader = dbcmd.ExecuteReader();

        resultFunc(reader);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void NonSelectQuery(String query, noResultDelegate resultFunc)
    {
        open_db("boxDB.db");
        IDbCommand dbcmd = dbconn.CreateCommand();

        dbcmd.CommandText = query;
        
        dbcmd.ExecuteNonQuery();

        resultFunc();

        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    
    /*
    public List<Quiz> get_all_quiz()
    {
        open_db("boxDB.db");
        IDbCommand dbcmd = dbconn.CreateCommand();
        
        
        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            // dbcmd.CommandText = "SELECT quiz_index,quiz_subject_en,quiz_content_en,quiz_question,quiz_answer FROM match_game";
            dbcmd.CommandText = "SELECT quiz_index,quiz_subject,quiz_content,quiz_question,quiz_answer FROM match_game";
            Debug.Log("korean");
        }
        else
        {
            // dbcmd.CommandText = "SELECT quiz_index,quiz_subject,quiz_content,quiz_question,quiz_answer FROM match_game";
            dbcmd.CommandText = "SELECT quiz_index,quiz_subject_en,quiz_content_en,quiz_question,quiz_answer FROM match_game";

        }

        IDataReader reader = dbcmd.ExecuteReader();
        
        List<Quiz> quiz_list = new List<Quiz>();

        //select *,ifnull(sum(score.score_num),0) from match_game LEFT join score ON match_game.quiz_index = score.quiz_index;
        //select *, (select sum(score_num) From score where quiz_index = match_game.quiz_index) from match_game;

        while (reader.Read())
        {
            Quiz quiz = new Quiz(reader.GetInt32(0), "Master", reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), 0);
            quiz_list.Add(quiz);

            Debug.Log("quiz = " + quiz.ToString());
        }


        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return quiz_list;
    }

    public Quiz get_quiz(int index)
    {


        open_db("boxDB.db");

        IDbCommand dbcmd = dbconn.CreateCommand();
        //dbcmd.CommandText = "SELECT * FROM match_game WHERE quiz_index = " + index;
        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            // dbcmd.CommandText = "SELECT quiz_index,quiz_subject_en,quiz_content_en,quiz_question,quiz_answer FROM match_game";
            dbcmd.CommandText = "SELECT quiz_index,quiz_subject,quiz_content,quiz_question,quiz_answer FROM match_game WHERE quiz_index = " + index;
            Debug.Log("korean");
        }
        else
        {
            // dbcmd.CommandText = "SELECT quiz_index,quiz_subject,quiz_content,quiz_question,quiz_answer FROM match_game";
            dbcmd.CommandText = "SELECT quiz_index,quiz_subject_en,quiz_content_en,quiz_question,quiz_answer FROM match_game WHERE quiz_index = " + index;

        }
        IDataReader reader = dbcmd.ExecuteReader();

        Quiz quiz = new Quiz();
        if (reader.Read())
        {

            quiz = new Quiz(reader.GetInt32(0), "Master", reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), 0); //new Quiz(reader.GetInt32(0),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetString(1),0);

            Debug.Log("quiz = " + quiz.ToString() );
        }
  

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return quiz;
    }

	public bool insert_quiz(Quiz quiz)
    {

        //string conn = "URI=file:" + Application.streamingAssetsPath + "/boxDB.db";

        string user_id = PlayerPrefs.GetString("user_id");

        open_db("boxDB.db");

        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "INSERT INTO match_game(user_id,quiz_subject,quiz_content,quiz_question,quiz_answer) VALUES ('"+ user_id + "','"+ quiz.quiz_subject + "','" + quiz.quiz_content + "','"+ quiz.quiz_question+"','" + quiz.quiz_answer +"')";

        dbcmd.ExecuteNonQuery();
        
        
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return true;
    }

    public bool update_quiz(int index,Quiz quiz)
    {


        open_db("boxDB.db");

        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "UPDATE match_game SET quiz_subject = '" + quiz.quiz_subject + "',quiz_content = '" + quiz.quiz_content + "',quiz_question = " + quiz.quiz_question + "',quiz_answer = " + quiz.quiz_answer +"' WHERE quiz_index="+index;

        dbcmd.ExecuteNonQuery();


        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return true;
    }

    public bool delete_quiz(int index)
    {


        open_db("boxDB.db");

        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE FROM match_game WHERE quiz_index="+index;

        dbcmd.ExecuteNonQuery();


        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return true;
    }
    */
}
