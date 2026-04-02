using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    
    [HideInInspector] public MoveController move;
    [HideInInspector] public DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    
    void Update()
    {
        move.Movement();

        if (inp.dashPressed)
        {
            fsm.SetGameState(PlayerState.Dash);
        }
    }
    
}
