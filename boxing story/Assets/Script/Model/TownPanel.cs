using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TownPanel : MonoBehaviour {

    public Text yearText;
    public Text goldText;
    public Text trophyText;


	// Use this for initialization
	void Start () {
		
	}
	
    public void SetYearText(string txt)
    {
        yearText.text = txt;
    }
    public void SetGoldText(string txt)
    {
        goldText.text = txt;
    }
    public void SetTrophyText(string txt)
    {
        trophyText.text = txt;
    }

}
