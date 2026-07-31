using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    Animator anim;
    
    public PlayerState_Idle idle;
    public PlayerState_Move move;
    
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
        // Player State Init
        idle.Initialize(this);
        move.Initialize(this);
    }

    void Start()
    {
        SwitchOn(idle);
    }
}
