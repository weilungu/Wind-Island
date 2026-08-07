using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    PlayerStateContext ctx;
    [SerializeField] PlayerState[] states;
    
    PlayerInput input;
    SpriteRenderer sprite;
    Animator anim;
    
    New_PlayerController player;
    PlayerMove move;
    PlayerDash dash;
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        
        player = GetComponent<New_PlayerController>();
        move = GetComponent<PlayerMove>();
        dash = GetComponent<PlayerDash>();
        
        ctx = new PlayerStateContext(
            input: input, 
            sprite: sprite, 
            anim: anim,
            
            player: player, 
            move: move,
            dash: dash
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
