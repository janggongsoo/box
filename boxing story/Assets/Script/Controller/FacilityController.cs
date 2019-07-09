using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class FacilityController : MonoBehaviour {

    Facility selectFacility;
    InputController inputController;
    TileController tileController;

    List<SellFacility> Sellers = new List<SellFacility>();
    List<GymFacility> Gyms = new List<GymFacility>();
    HomeFacility home;

    void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
        tileController = GameObject.Find("TileController").GetComponent<TileController>();
        home = new HomeFacility(tileController);
        tileController.getTile(0, 0).ChangeFacility(home);
        
    }
    public Facility getSelectFacility()
    {
        return selectFacility;
    }
    public void AddSellFacility(SellFacility item)
    {
        Sellers.Add(item);
        sortSellFacility();
        facilityPathRefresh();
    }
    public void RemoveSellFacility(SellFacility item)
    {
        Sellers.Remove(item);
        facilityPathRefresh();
    }
    public void sortSellFacility()
    {
        Sellers.Sort((SellFacility a, SellFacility b) => b.getFame().CompareTo(a.getFame()));
    }
    public void sortGymFacility()
    {
        Gyms.Sort((GymFacility a, GymFacility b) => b.getFame().CompareTo(a.getFame()));

    }
    public void AddGymFacility(GymFacility item)
    {
        Gyms.Add(item);
        sortGymFacility();
        facilityPathRefresh();
    }
    public void RemoveGymFacility(GymFacility item)
    {
        Gyms.Remove(item);
        facilityPathRefresh();
    }
    public void BuildButton(int _id)
    {
        DBscript db = new DBscript();

        db.SelectQuery("SELECT * FROM FACILITY WHERE ID =" + _id, GetFacilityData);

    }
    public List<SellFacility> getSellFacility()
    {
        return Sellers;
    }
    public List<GymFacility> getGymFacility()
    {
        return Gyms;
    }
    private void facilityPathRefresh()
    {
        Debug.Log("facilityPathRefresh");
        foreach (SellFacility fac in Sellers)
        {
            fac.RefreshPath();
        }
        foreach (GymFacility fac in Gyms)
        {
            fac.RefreshPath();
        }
        home.RefreshPath();
    }

    private void GetFacilityData(IDataReader reader)
    {
        Facility build = new NullFacility();
        if (reader.Read())
        {
            
        }
        selectFacility = build;
        inputController.SetState(StateType.Install);
    }
    public void SetInstallFacility(Facility build)
    {
        selectFacility = build;
        inputController.SetState(StateType.Install);

    }
    public void SetSelectFacility(Facility _facility)
    {
        selectFacility = _facility;
    }
}
