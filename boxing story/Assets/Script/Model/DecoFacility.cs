using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoFacility : Facility
{
    int range;
    BuffType buffType;
    int buffValue;

    TileController tileController;

    public DecoFacility(FacilityType _type,int _range,BuffType _buffType, int _buffValue)
    {
        type = _type;
        facilitySprite = SpriteManager.Instance.getFacilitySprite(_type);

        tileController = GameObject.Find("TileController").GetComponent<TileController>();
        
        range = _range;
        buffType = _buffType;
        buffValue = _buffValue;
    }
    public DecoFacility(int id, string name, int facility_type, int need_price, int abill_type, int value, Sprite img)
    {
        this.name = name;
        this.facility_type = facility_type;
        this.need_price = need_price;
        this.abill_type = abill_type;
        this.value = value;
        facilitySprite = img;

        tileController = GameObject.Find("TileController").GetComponent<TileController>();

    }
    public override bool isNull()
    {
        return false;
    }
    public override void ApplyBuff(Dictionary<BuffType, int> buffList)
    {

    }
    public override bool InteractWithCharacter(Character character)
    {

        return false;
    }
    
    public override bool isEmpty()
    {
        return false;
    }
    public override void EnterFacility(Character character)
    {
    }
    public override void ExitFacility()
    {
    }
    public override void MakeFacility()
    {
        int posX = positionTile.GetPosX();
        int posY = positionTile.GetPosY();

        tileController.SetTileBuff(range, posX, posY, buffType, buffValue);
    }
    public override void RemoveFacility()
    {
        int posX = positionTile.GetPosX();
        int posY = positionTile.GetPosY();

        tileController.SetTileBuff(range, posX, posY, buffType, -1* buffValue);

        GameObject.Destroy(facilityObj);
    }
    public override int GetPathValue(int _x, int _y)
    {
        return 0;
    }
}
