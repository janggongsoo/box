using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullFacility : Facility {

    public override bool isNull()
    {
        return true;
    }
    public override bool InteractWithCharacter(Character character)
    {
        return false;
    }
    public override void ApplyBuff(Dictionary<BuffType, int> buffList)
    {
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
    }
    public override int GetPathValue(int _x, int _y)
    {
        return 0;
    }
    public override void RemoveFacility()
    {
    }
}
