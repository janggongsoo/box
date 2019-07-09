using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public GameObject touristCharacterPrefab;
    public GameObject townCharacterPrefab;
    
    // Use this for initialization
    void Start () {
        MakeCharacter(touristCharacterPrefab);
      //  MakeCharacter(townCharacterPrefab);
     //   MakeCharacter(touristCharacterPrefab);
      //  MakeCharacter(townCharacterPrefab);
    }
	private void MakeCharacter(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        
        
    }
    
}
