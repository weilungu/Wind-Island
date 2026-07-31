using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Animator anim;
    
    [SerializeField] Player_State[] states;
    
    void Awake()
    {
        // anim = GetComponentInChildren<Animator>();
        stateTable = new Dictionary<Type, IState>(states.Length);
        
        foreach (Player_State st in states)
        {
            st.Initialize(this);
            stateTable.Add(st.GetType(), st);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
