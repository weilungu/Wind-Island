using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_PlayerController : MonoBehaviour
{
    PlayerInput input;
    PlayerMove move;
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
    }


    // === Self API === //
    public void Flip(SpriteRenderer sprite)
    {
        Vector2 dir = move.Direction;
        if (dir.x != 0f)
            sprite.flipX = dir.x < 0f;
    }
    
    // Pause and Unpause
    public void EnableGameplay() => input.EnableGameplay();
    public void DisableGameplay() => input.DisableGameplay();
}
