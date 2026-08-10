using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currState;
    protected Dictionary<Type, IState> stateTable;

    void Update()
    {
        currState?.LogicalUpdate();
    }

    void FixedUpdate()
    {
        currState?.PhysicalUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        if (newState is null)
            throw new ArgumentNullException(nameof(newState));

        currState = newState;
        currState.Enter();
    }

    public void SwitchState(IState newState)
    {
        if (newState is null)
            throw new ArgumentNullException(nameof(newState));

        currState?.Exit();
        SwitchOn(newState);
    }
    
    public void SwitchState(Type newStateType)
    {
        if (stateTable is null || !stateTable.TryGetValue(newStateType, out IState newState))
            throw new KeyNotFoundException($"State not found: {newStateType?.Name}");

        SwitchState(newState);
    }
}
