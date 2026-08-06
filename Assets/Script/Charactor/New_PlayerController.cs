using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_PlayerController : MonoBehaviour
{
    PlayerInput input;
    
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        input.EnableGameplayInput();
    }

    
    // === Self API === //
    public Vector2 MoveVector() 
        => new Vector2(input.Horizontal, input.Vertical).normalized;
}
