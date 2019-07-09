using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {


    public Text price_text;
    public Button button;

    Facility facility;

    FacilityController facilityController;
    UIController uiController;
    Town town;

    bool isCanUse = false;

	// Use this for initialization
	void Start () {
        facilityController = GameObject.Find("FacilityController").GetComponent<FacilityController>();
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        //town = GameObject.Find("Town").GetComponent<Town>();
        button = GetComponent<Button>();

    }
    void OnEnable() {

        CheckEnable();
    }


    public void setButton(int id,string name,int facility_type,int need_price,int abill_type,int value,Sprite img)
    {
        facility = new NullFacility();

        price_text.text = need_price.ToString();
        button.image.sprite = img;


        if (facility_type == 1)
        {
            facility = new GymFacility(id, name, facility_type, need_price, abill_type, value,img);
        }
        else if (facility_type == 2)
        {
            facility = new SellFacility(id, name, facility_type, need_price, abill_type, value, img);

        }
        else if (facility_type == 3)
        {
            facility = new DecoFacility(id, name, facility_type, need_price, abill_type, value, img);

        }
        else if (facility_type == 4)
        {
            facility = new EtcFacility(id, name, facility_type, need_price, abill_type, value, img);

        }
        CheckEnable();
    }
    public void CheckEnable()
    {
        if (town == null)
        {
            town = GameObject.Find("Town").GetComponent<Town>();
        }
        int townMoney = town.Money;
        if(townMoney< facility.getNeedPrice())
        {
            isCanUse = false;
            button.image.color = new Color32(255, 120, 120, 255);
        }
        else
        {
            isCanUse = true;
            button.image.color = new Color32(255, 255,255, 255);
        }
    }

    public void OnClick()
    {
        if(facilityController.getSelectFacility() == facility)
        {
            if (isCanUse)
            {
                uiController.DisablePanel();
                facilityController.SetInstallFacility(facility);

            }
        }else
        {
            facilityController.SetSelectFacility(facility);
            uiController.SetBottomInfoWithFacility(facility);
        }
    }
}
