
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateIdle : CharacterState
{
    public override bool Move(Character _character, Tile dest)
    {
        if (dest == null)
        {
            return true;
        }
        Transform trans = _character.transform;
        Vector3 desPos = dest.GetTileObject().transform.position + new Vector3(0, 0, -1.5f);
        Vector3 direct = desPos - trans.position;

        trans.Translate(direct.normalized * 0.4f *Time.deltaTime);
        float diffPos = Vector3.Distance(desPos, trans.position);

        if (diffPos > 0.1f)
            return false;
        return true;
    }

    public override Tile FindNext(Character _character, TileController tileController)
    {
        IntVector2[] checkPos = new IntVector2[4]
           {
            new IntVector2(0,1),new IntVector2(1,0),new IntVector2(0,-1),new IntVector2(-1,0)
           };
        int startIndex = Random.Range(0, 4);
        Tile currentTile = _character.getCurrentTile();

        int checkX = currentTile.GetPosX() + checkPos[startIndex].x;
        int checkY = currentTile.GetPosY() + checkPos[startIndex].y;

        for (int i = 0; i < 4; i++)
        {
            int index = (startIndex + i) % 4;
            int tempX = currentTile.GetPosX() + checkPos[index].x;
            int tempY = currentTile.GetPosY() + checkPos[index].y;

            if (isRangeOut(tempX,tempY))
            {
                continue;
            }
            if (tileController.getTile(tempX, tempY).getFacility().isNull())
            {
                _character.anim.SetFloat("PosX", checkPos[index].x);
                _character.anim.SetFloat("PosY", checkPos[index].y);
                return tileController.getTile(tempX, tempY);
            }

            checkX = tempX;
            checkY = tempY;
            
        }

        //사방이 건물로 막혔을 경우엔 마지막 체크지점을 반환.
        return tileController.getTile(checkX, checkY);
    }

    public override CharacterState CheckTile(Character _character)
    {
        Tile dest = _character.FindDestination();
        if(dest != null)
        {
            _character.setDestTile(dest);
            return CharacterState.stateMove;
        }

        return CharacterState.stateIdle;
    }
}
