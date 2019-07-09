using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Price,Envir
}


public class Tile {
   
    int posX;
    int posY;

    Facility facility;
    GameObject tileObj;

    Dictionary<BuffType, int> buffList = new Dictionary<BuffType, int>();
    
    public Tile()
    {
        facility = new NullFacility();
    }
    public Tile(int y,int x, GameObject _obj)
    {
        posX = x;
        posY = y;
        tileObj = _obj;
        facility = new NullFacility();
    }

    public bool ChangeFacility(Facility _facility)
    {
        if (facility.isNull())
        {

            facility = _facility;
            _facility.CreateFacility(this);

            ApplyFacility();
            return true;
        }else
        {
            if (_facility.isNull())
            {
                facility.RemoveFacility();
                facility = _facility;
                //_facility.CreateFacility(this);

                ApplyFacility();
            }
        }
        return false;
    }
    public void AddBuffList(BuffType _type,int value)
    {
        if (buffList.ContainsKey(_type))
        {
            buffList[_type] += value;
        }else
        {
            buffList.Add(_type, value);
        }
        ApplyFacility();
    }
    public void RemoveBuffList(BuffType _type, int value)
    {
        if (buffList.ContainsKey(_type))
        {
            buffList[_type] -= value;
            if (buffList[_type] <= 0)
            {
                buffList.Remove(_type);
            }
        }
        

        ApplyFacility();
    }

    public Dictionary<BuffType, int> GetBuffList()
    {
        return buffList;
    }

    public void GetInformation()
    {
        Debug.Log("posX = " + posX + " posY = " + posY);
    }


    void ApplyFacility()
    {
        facility.ApplyBuff(buffList);
    }
    public Facility getFacility()
    {
        return facility;
    }

    public GameObject GetTileObject()
    {
        return tileObj;
    }
    public int GetPosX()
    {
        return posX;
    }
    public int GetPosY()
    {
        return posY;
    }

}
