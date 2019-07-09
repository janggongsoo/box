using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializebleSpritePair
{
    public FacilityType keys;
    public Sprite valus;
    

}

public class SpriteManager : Singleton<SpriteManager> {
    public Sprite borderSprite;

    // public Sprite[] facilitySprites;

    public SerializebleSpritePair[] facilitySprites;

    public Sprite getFacilitySprite(FacilityType key)
    {
        for(int i = 0; i < facilitySprites.Length; i++)
        {
            if (facilitySprites[i].keys.Equals(key))
            {
                return facilitySprites[i].valus;
            }
        }
        Debug.Log("sprite not exist");
        return null;
    }
    
}
