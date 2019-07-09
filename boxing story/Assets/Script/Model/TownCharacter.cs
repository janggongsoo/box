using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCharacter : Character {

    Facility homeFacility;

    public void setHomeFacility(Facility _home)
    {
        homeFacility = _home;
    }
    public override Tile FindDestination()
    {
        List<GymFacility> builds = facilityController.getGymFacility();;
        if (getHp()<10)
        {
            return homeFacility.getPositionTile();
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
