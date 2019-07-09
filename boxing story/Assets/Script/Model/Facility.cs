using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum FacilityType
{
    Empty, Room1, Seller, Playground, Flower
}

public abstract class Facility {
    
    public FacilityType type;

    public Sprite facilitySprite;

    protected GameObject facilityObj;

    float useTime = 4.0f;
    int useAnimation = 0;

    protected Tile positionTile;

    protected string name;
    protected int facility_type;
    protected int need_price;
    protected int abill_type;
    protected int value;

    public int fame = 0;
    
    public float getUseTime()
    {
        return useTime;
    }
    public int getUseAnimaion()
    {
        return useAnimation;
    }
    public string getName()
    {
        return name;
    }
    public int getFacilityType()
    {
        return facility_type;
    }
    public int getAbillType()
    {
        return abill_type;
    }
    public int getValue()
    {
        return value;
    }
    public int getNeedPrice()
    {
        return need_price;
    }
    public void CreateFacility(Tile tile)
    {
        GameObject obj = new GameObject("facility");
        obj.transform.SetParent(tile.GetTileObject().transform);
        obj.AddComponent<SpriteRenderer>().sprite = facilitySprite;
        obj.transform.localPosition = new Vector3(0, 0.5f, -1);
        facilityObj = obj;
        positionTile = tile;

        Town.Instance.Money -= need_price; 

        MakeFacility();
    }
    public Tile getPositionTile()
    {
        return positionTile;
    }
    public abstract void MakeFacility();
    public abstract bool isEmpty();
    public abstract bool isNull();
    public abstract void ApplyBuff(Dictionary<BuffType, int> buffList);
    public abstract bool InteractWithCharacter(Character character);
    public abstract void EnterFacility(Character character);
    public abstract void ExitFacility();
    public abstract int GetPathValue(int _x,int _y);
    public abstract void RemoveFacility();
}
