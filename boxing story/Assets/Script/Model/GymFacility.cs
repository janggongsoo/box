using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Str,Spd
}

public class GymFacility : Facility
{
    public int fame = 0;
    AbilityType ability = AbilityType.Str;
    int abiliValue = 20;

    Character user;

    string textPoolName = "TextPool";

    int[,] path = new int[Constants.Height, Constants.Width];

    FacilityController facilityController;
    TileController tileController;
    public GymFacility(FacilityType _type)
    {
        type = _type;
        facilitySprite = SpriteManager.Instance.getFacilitySprite(_type);
        facilityController = GameObject.Find("FacilityController").GetComponent<FacilityController>();
        tileController = GameObject.Find("TileController").GetComponent<TileController>();

    }
    public GymFacility(int id, string name, int facility_type, int need_price, int abill_type, int value, Sprite img)
    {
        this.name = name;
        this.facility_type = facility_type;
        this.need_price = need_price;
        this.abill_type = abill_type;
        this.value = value;
        facilitySprite = img;
        facilityController = GameObject.Find("FacilityController").GetComponent<FacilityController>();
        tileController = GameObject.Find("TileController").GetComponent<TileController>();

    }
    public void setFame(int _fame)
    {
        fame = _fame;
        facilityController.sortGymFacility();
    }
    public int getFame()
    {
        return fame;
    }
    public override void MakeFacility()
    {

        facilityController.AddGymFacility(this);
    }
    public override bool isNull()
    {
        return false;
    }
    public override void ApplyBuff(Dictionary<BuffType, int> buffList)
    {
        if (buffList.ContainsKey(BuffType.Envir))
        {

            setFame(buffList[BuffType.Envir]);
            //fame = buffList[BuffType.Envir];
            ShowMoveText("Fame " + buffList[BuffType.Envir]);
        }
        Debug.Log("fame = " + fame);

    }
    private void ShowMoveText(string _text)
    {

        MoveText textObj = PoolManager.Instance.getObject(textPoolName, facilityObj.transform).GetComponent<MoveText>();
        textObj.setTextMessage(_text);
        textObj.transform.position = facilityObj.transform.position + new Vector3(0, 0, -1);
        textObj.gameObject.SetActive(true);
    }
    public override bool InteractWithCharacter(Character character)
    {
        if(ability == AbilityType.Str)
        {
            user.addStr(abiliValue);
        }
        else if(ability == AbilityType.Spd)
        {
            user.addSpd(abiliValue);
        }
        return false;

    }
    public override bool isEmpty()
    {
        return user==null;
    }
    public override void EnterFacility(Character character)
    {
        user = character;
    }
    public override void ExitFacility()
    {
        user = null;
    }
    public override int GetPathValue(int _x, int _y)
    {
        return path[_y, _x];
    }
    public override void RemoveFacility()
    {
        GameObject.Destroy(facilityObj);

        facilityController.RemoveGymFacility(this);
    }
    public void RefreshPath()
    {
        Queue<Tile> pathCheckList = new Queue<Tile>();
        //Stack<Tile> pathCheckList = new Stack<Tile>();
        bool[,] isCheck = new bool[Constants.Height, Constants.Width];
        IntVector2[] checkPos = new IntVector2[4]
        {
            new IntVector2(0,1),new IntVector2(1,0),new IntVector2(0,-1),new IntVector2(-1,0)
        };
        int count = 0;
        Tile firstTile = positionTile;
        pathCheckList.Enqueue(positionTile);
        path[positionTile.GetPosY(), positionTile.GetPosX()] = count;

        while (pathCheckList.Count != 0)
        {
            Tile tile = pathCheckList.Dequeue();
            int _x = tile.GetPosX();
            int _y = tile.GetPosY();
            isCheck[_y, _x] = true;

            for (int i = 0; i < 4; i++)
            {
                int tempX = _x + checkPos[i].x;
                int tempY = _y + checkPos[i].y;
                if (tempX < 0 || tempY < 0 || tempX >= 32 || tempY >= 32)
                {
                    continue;
                }
                if (isCheck[tempY, tempX] == false)
                {
                    if (tileController.getTile(tempX, tempY).getFacility().isNull())
                    {

                        Tile nextTile = tileController.getTile(tempX, tempY);
                        pathCheckList.Enqueue(nextTile);
                        path[tempY, tempX] = path[_y, _x] + 1;

                    }
                    else
                    {
                        path[tempY, tempX] = 1000;

                    }

                    isCheck[tempY, tempX] = true;
                }
            }

        }
    }
}
