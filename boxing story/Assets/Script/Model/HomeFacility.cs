using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeFacility : Facility
{

    int[,] path = new int[Constants.Height, Constants.Width];
    
    TileController tileController;
    public HomeFacility(TileController _tileController)
    {

        tileController = _tileController;

    }
    public override void ApplyBuff(Dictionary<BuffType, int> buffList)
    {
    }

    public override void EnterFacility(Character character)
    {
        GameObject.Destroy(character.gameObject);
    }

    public override void ExitFacility()
    {
    }

    public override int GetPathValue(int _x, int _y)
    {

        Debug.Log("GetPathValue" + path[_y, _x]);
        return path[_y, _x];
    }

    public override bool InteractWithCharacter(Character character)
    {
        return false;
    }

    public override bool isEmpty()
    {
        return true;
    }

    public override bool isNull()
    {
        return false;
    }

    public override void MakeFacility()
    {
        RefreshPath();
    }
    public override void RemoveFacility()
    {
    }
    public void RefreshPath()
    {
        Queue<Tile> pathCheckList = new Queue<Tile>();
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
