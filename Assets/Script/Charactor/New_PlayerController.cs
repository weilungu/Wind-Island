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
    public Vector2 MoveDirection
        => new Vector2(input.Horizontal, input.Vertical).normalized;

    public void Flip(SpriteRenderer sprite)
    {
        Vector2 dir = MoveDirection;
        if (dir.x != 0f)
            sprite.flipX = dir.x < 0;
    }
}
