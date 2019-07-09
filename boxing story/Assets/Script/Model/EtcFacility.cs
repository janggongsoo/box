using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcFacility : Facility
{
    Character master;

    bool isUse = false;
    int recovery = 10;

    public EtcFacility(FacilityType _type)
    {
        type = _type;
        facilitySprite = SpriteManager.Instance.getFacilitySprite(_type);

    }
    public EtcFacility(int id, string name, int facility_type, int need_price, int abill_type, int value, Sprite img)
    {
        this.name = name;
        this.facility_type = facility_type;
        this.need_price = need_price;
        this.abill_type = abill_type;
        this.value = value;
        facilitySprite = img;

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

        character.addHp(recovery);

        return !character.isFullHp();

    }
    
    public override bool isEmpty()
    {
        return isUse;
    }
    public override void EnterFacility(Character character)
    {
    }
    public override void ExitFacility()
    {

    }
    public override void MakeFacility()
    {
    }
    public override void RemoveFacility()
    {

        GameObject.Destroy(facilityObj);
    }
    public override int GetPathValue(int _x, int _y)
    {
        return 0;
    }
}
