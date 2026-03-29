using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    
    MoveController move;
    DashController dash;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    
    void Update()
    {
        move.Movement();
        dash.Dash();
    }
    
}
