using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristCharacter : Character
{
    public override Tile FindDestination()
    {
        
        List<SellFacility> builds = facilityController.getSellFacility();
        if (!isEmptyMoney(100))
        {
            return tileController.getTile(0, 0);
        }
        foreach (Facility build in builds)
        {
            if (build == beforeFacility)
            {
                continue;
            }
            if (build.isEmpty())
                return build.getPositionTile();
        }
        return null;
    }
}
