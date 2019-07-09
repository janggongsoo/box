using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Singleton<Town> {
    int money = 2200;
    int fame;
    int trophy;
    int time;

    public TownPanel panel;

    public void Start()
    {
        panel = GameObject.Find("TownPanel").GetComponent<TownPanel>();


    }

    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
            panel.SetGoldText(money.ToString());
        }
    }

    public int Fame
    {
        get
        {
            return fame;
        }

        set
        {
            fame = value;
        }
    }

    public int Trophy
    {
        get
        {
            return trophy;
        }

        set
        {
            trophy = value;
            panel.SetTrophyText(trophy.ToString());
        }
    }

    public int Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
            panel.SetYearText(time.ToString());
        }
    }
}
