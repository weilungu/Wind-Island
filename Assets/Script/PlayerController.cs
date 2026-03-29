using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputController inp;
    MoveController move;

    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
    }

    
    void Update()
    {
        move.Movement();
        
        inp.GetDashInput();
    }
    
}
