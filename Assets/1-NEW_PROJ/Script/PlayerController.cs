using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement move;

    
    // == Unity API == //
    void Awake()
    {
        move = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        move.Movement();
    }
}
