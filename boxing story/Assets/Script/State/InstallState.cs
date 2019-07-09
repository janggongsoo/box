using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallState : State
{
    Facility selectFacility;
    TileController tileController;
    FacilityController facilityController;
    InputController input;

    GameObject mousePointer;


    public InstallState(TileController _tile, FacilityController _facilityController, InputController _inputController)
    {
        tileController = _tile;
        selectFacility = new NullFacility();
        facilityController = _facilityController;
        input = _inputController;

        mousePointer = new GameObject("buildPointer");
        mousePointer.AddComponent<SpriteRenderer>().sprite = selectFacility.facilitySprite;

        Color facilColor = mousePointer.GetComponent<SpriteRenderer>().color;
        facilColor.a = 0.5f;
        mousePointer.GetComponent<SpriteRenderer>().color = facilColor;
        
    }
    public void SetSelectFacility(Facility _selectFacility)
    {
        selectFacility = _selectFacility;
        mousePointer.GetComponent<SpriteRenderer>().sprite = _selectFacility.facilitySprite;
    }
    public override void UpdateClick()
    {

        Vector3 _pos = mousePointer.transform.position;
        int _posX = Mathf.FloorToInt(_pos.x);
        int _posY = Mathf.FloorToInt(_pos.y * 2);

        int _x = (_posY + _posX) >> 1;
        int _y = (_posY - _posX) >> 1;

        bool isInstall = tileController.getTile(_x, _y).ChangeFacility(selectFacility);

        if (isInstall)
        {

            input.SetState(StateType.Normal);

        }
    }

    public override void UpdateDrag(Vector3 _pos)
    {
        Vector3 currentPos = currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 0;

        Camera.main.transform.Translate(_pos - currentPos);
    }

    public override void UpdateMove(Vector3 _pos)
    {
        Vector3 movePos = getTileCenterPosFromMouse(_pos);
        movePos.y += 0.5f;
        mousePointer.transform.position = movePos;
    }
    public override void EnterState()
    {
        SetSelectFacility(facilityController.getSelectFacility());
        mousePointer.SetActive(true);
    }

    public override void ExitState()
    {
        mousePointer.SetActive(false);
    }

}
