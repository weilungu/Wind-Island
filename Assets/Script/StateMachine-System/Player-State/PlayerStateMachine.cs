using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    PlayerStateContext ctx;
    [SerializeField] PlayerState[] states;
    
    PlayerInput input;
    Animator anim;
    New_PlayerController player;
    
    PlayerMove move;
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<New_PlayerController>();
        
        move = GetComponent<PlayerMove>();
        
        ctx = new PlayerStateContext(
            input: input,
            anim: anim,
            player: player,
            
            move: move
        );
        
        stateTable = new Dictionary<Type, IState>(states.Length);
        
        foreach (PlayerState st in states)
        {
            st.Initialize(this, ctx);
            stateTable.Add(st.GetType(), st);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);
    }
}
