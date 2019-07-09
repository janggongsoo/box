using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacilityInfoPanel : MonoBehaviour {
    public Text txt_name;
    public Image img;
    public Text txt_facilityType;
    public Text txt_abill;
    public Text txt_value;



    public void SetDate(Facility facility)
    {
        txt_name.text = facility.getName();
        img.sprite = facility.facilitySprite;
        txt_facilityType.text = facility.getFacilityType().ToString();
        txt_abill.text = facility.getAbillType().ToString();
        txt_value.text = facility.getValue().ToString();
    }
}
