using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currState;

    void Update()
    {
        currState.LogicalUpdate();
    }

    void FixedUpdate()
    {
        currState.PhysicalUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        currState = newState;
        currState.Enter();
    }

    public void SwitchState(IState newState)
    {
        currState.Exit();
        SwitchOn(newState);
    }
}
