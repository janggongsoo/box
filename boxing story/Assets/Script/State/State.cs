using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Normal,Install,Menu
}

public abstract class State  {

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateMove(Vector3 _pos);
    public abstract void UpdateClick();
    public abstract void UpdateDrag(Vector3 _pos);
    
    protected Vector3 getTileCenterPosFromMouse(Vector3 _mousePos)
    {
        _mousePos.y = _mousePos.y * 2;
        int mPosX = Mathf.FloorToInt(_mousePos.x);
        int mPosY = Mathf.FloorToInt(_mousePos.y);

        // 홀짝 구분 / 노출되는 타일들의 중앙점은 합이 짝수다.
        int checkEven = (mPosX + mPosY) & 1;
        // 홀수라면 remainX에 곱하여 양수로 바꿔준다.
        int tempEven = (checkEven * -2) + 1;

        // 홀수라면 짝수로 기준센터 이동.
        mPosX += checkEven;

        float remainX = _mousePos.x - mPosX;
        float remainY = _mousePos.y - mPosY;

        // 소수점 이하의 수를 더하여, 1보다 크면 이동.
        float remainSum = (tempEven * remainX) + remainY;
        // 더한 값을 내림하여, 1.0 이상이면1로 만들어 최종 계산식에 사용.
        int floorSum = Mathf.FloorToInt(remainSum);

        // 더한 값이 1.0 이상이고, checkEven이 짝수라면 x+1,y+1  (floorSum = 1 , tempEven = 1)
        // 더한 값이 1.0 이상이고, checkEven이 홀수라면 x-1,y+1  (floorSum = 1 , tempEven = -1)
        // 더한 값이 1.0 이하라면, x,y  (floorSum = 0)
        Vector3 result = new Vector3(mPosX + (floorSum * tempEven), (mPosY + floorSum) * 0.5f, -1);

        //Debug.Log("_mousePos = " +_mousePos + " mPosX = " + mPosX + " mPosY = " + mPosY + " checkEven = " + checkEven + " tempEven = " + tempEven + " remainX = " + remainX + " remainY = " + remainY + " remainSum = " + remainSum +" FloorSum = " + floorSum + " result = " + result);

        return result;
    }
}
