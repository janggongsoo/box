using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour {
    
    Vector3 lastMousePos;

    TileController tileController;
    FacilityController facilityController;

    Dictionary<StateType,State> stateMap = new Dictionary<StateType, State>();

    State state;
	// Use this for initialization
	void Start () {

        tileController = GameObject.Find("TileController").GetComponent<TileController>();
        facilityController = GameObject.Find("FacilityController").GetComponent<FacilityController>();
        CreateState();
        
        SetState(StateType.Normal);
    }
	void CreateState()
    {
        stateMap.Add(StateType.Normal, new NormalState(tileController));
        stateMap.Add(StateType.Install, new InstallState(tileController,facilityController, this));
        stateMap.Add(StateType.Menu, new MenuState());
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            state.UpdateDrag(lastMousePos);
        }

        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastMousePos.z = 0;

        state.UpdateMove(lastMousePos);

        if (Input.GetMouseButtonDown(0))
        {
            state.UpdateClick();
        }
    }
    public void SetState(StateType _type)
    {
        if (state != null)
        {
            state.ExitState();

        }
        state = stateMap[_type];
        state.EnterState();
    }
    public void SetStateMenu()
    {
        SetState(StateType.Menu);
    }
    public void SetStateNormal()
    {
        SetState(StateType.Normal);
    }
    
}
