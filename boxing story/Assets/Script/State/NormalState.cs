using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : State
{
    TileController tileController;
    
    GameObject mousePointer;
    UIController uiController;

    public NormalState(TileController _tile)
    {
        tileController = _tile;
        mousePointer = new GameObject("MousePointer");
        uiController = GameObject.Find("UIController").GetComponent<UIController>();

        mousePointer.AddComponent<SpriteRenderer>().sprite = SpriteManager.Instance.getFacilitySprite(FacilityType.Empty);

        Color facilColor = mousePointer.GetComponent<SpriteRenderer>().color;
        facilColor.a = 1.0f;
        mousePointer.GetComponent<SpriteRenderer>().color = facilColor;

    }
    public override void UpdateClick()
    {
        Vector3 _pos = mousePointer.transform.position;
        int _posX = Mathf.FloorToInt(_pos.x);
        int _posY = Mathf.FloorToInt(_pos.y*2);

        int _x = (_posY + _posX) >> 1;
        int _y = (_posY - _posX) >> 1;

        tileController.getTile(_x, _y).GetInformation();
        if(!tileController.getTile(_x, _y).getFacility().isNull())
        {
            uiController.SetFacilityInfoPanel(tileController.getTile(_x, _y).getFacility());
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
        mousePointer.transform.position = getTileCenterPosFromMouse(_pos);
    }

    public override void EnterState()
    {
        mousePointer.SetActive(true);
    }

    public override void ExitState()
    {
        mousePointer.SetActive(false);
    }
}
