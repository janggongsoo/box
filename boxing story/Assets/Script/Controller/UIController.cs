using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject FacilityButton;

    public GameObject EtcFacilityPanel;
    public GameObject SellFacilityPanel;
    public GameObject GymFacilityPanel;
    public GameObject DecoFacilityPanel;

    public GameObject buildPanel;

    public Text bottom_txt;

    public FacilityInfoPanel facilityInfoPanel;
    public InputController inputController;

    // Use this for initialization
    void Start () {
        MakeFacilityButton();
	}
	
    private void MakeFacilityButton()
    {
        DBscript db = new DBscript();

        GameObject button = Instantiate(FacilityButton, EtcFacilityPanel.transform);
        BuildButton facilityBtn = button.GetComponent<BuildButton>();

        facilityBtn.setButton(0, "Remove", 0,0, 0, 0, null);


        db.SelectQuery("Select * From Facility", FacilityQueryExcute);
    }
    private void FacilityQueryExcute(IDataReader reader)
    {
        Debug.Log("FacilityQueryExcute");
        while (reader.Read())
        {
            int type = reader.GetInt32(2);
            // 1 - gym, 2 - store, 3 - deco, 4 - etc
            Sprite img = Resources.Load("Image/Map/" + reader.GetString(6),typeof(Sprite)) as Sprite;

            Transform parent = GymFacilityPanel.transform;
            if (type == 1)
            {
                parent = GymFacilityPanel.transform;
            }
            else if (type == 2)
            {
                parent = SellFacilityPanel.transform;

            }
            else if (type == 3)
            {
                parent = DecoFacilityPanel.transform;

            }
            else if (type == 4)
            {
                parent = EtcFacilityPanel.transform;

            }

            GameObject button = Instantiate(FacilityButton,parent);
            BuildButton facilityBtn = button.GetComponent<BuildButton>();
            
            facilityBtn.setButton(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), img);
        }
    }

    public void DisablePanel()
    {
        buildPanel.SetActive(false);
    }
    public void SetBottomInfoWithFacility(Facility facility)
    {
        bottom_txt.text = facility.getName();
    }
    public void SetFacilityInfoPanel(Facility facility)
    {
        inputController.SetState(StateType.Menu);
        facilityInfoPanel.gameObject.SetActive(true);
        facilityInfoPanel.SetDate(facility);

    }
}
