using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement move;
    PlayerDash dash;
    
    
    // == Unity API == //
    void Awake()
    {
        move = GetComponent<PlayerMovement>();
        dash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        if (dash.IsDashing) return;
        
        move.Movement();
    }
}
