using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    public override void UpdateClick()
    {
    }

    public override void UpdateDrag(Vector3 _pos)
    {
    }

    public override void UpdateMove(Vector3 _pos)
    {
    }
    public override void EnterState()
    {
        Time.timeScale = 0.00001f;
    }

    public override void ExitState()
    {
        Time.timeScale = 1;
    }
}
