using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    public override bool Move(Character _character, Tile dest)
    {
        Transform trans = _character.transform;
        Vector3 desPos = dest.GetTileObject().transform.position + new Vector3(0, 0, -1.5f);
        Vector3 direct = desPos - trans.position;
        
        trans.Translate(direct.normalized * 0.4f * Time.deltaTime);
        float diffPos = Vector3.Distance(desPos, trans.position);
        
        if (diffPos > 0.1f)
            return false;
        if(dest == _character.getDestTile())
        {
            if (!dest.getFacility().isNull())
            {
                if (dest.getFacility().isEmpty())
                {
                    _character.EnterFacility(dest.getFacility());
                }
            }

        }
        return true;
    }
    public override Tile FindNext(Character _character, TileController tileController)
    {
        IntVector2[] checkPos = new IntVector2[4]
           {
            new IntVector2(0,1),new IntVector2(1,0),new IntVector2(0,-1),new IntVector2(-1,0)
           };

        Tile currentTile = _character.getCurrentTile();
        Tile destTile = _character.getDestTile();
        int value = destTile.getFacility().GetPathValue(currentTile.GetPosX(), currentTile.GetPosY());
        
        int checkX = currentTile.GetPosX() + checkPos[0].x;
        int checkY = currentTile.GetPosY() + checkPos[0].y;

        for (int i = 0; i < 4; i++)
        {
            int tempX = currentTile.GetPosX() + checkPos[i].x;
            int tempY = currentTile.GetPosY() + checkPos[i].y;

            if (isRangeOut(tempX,tempY))
            {
                continue;
            }
            if(destTile.getFacility().GetPathValue(tempX, tempY) == value-1)
            {

                _character.anim.SetFloat("PosX", checkPos[i].x);
                _character.anim.SetFloat("PosY", checkPos[i].y);
                return tileController.getTile(tempX, tempY);
            }

            //그 어디에도 v-1구간이 없다면 가장 작은 값을 가진 타일로 이동한다.
            if (destTile.getFacility().GetPathValue(tempX, tempY) < destTile.getFacility().GetPathValue(checkX, checkY))
            {
                checkX = tempX;
                checkY = tempY;
            }
        }
        return tileController.getTile(checkX, checkY);
    }
    public override CharacterState CheckTile(Character _character)
    {
        Tile dest = _character.getDestTile();
        if (dest.getFacility().isEmpty() && !dest.getFacility().isNull())
        {
            return CharacterState.stateMove;
        }
        return CharacterState.stateIdle;
    }
}
