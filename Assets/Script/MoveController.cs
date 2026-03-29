using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    float vertical;
    float horizontal;
    
    [SerializeField] float speed;

    Vector2 GetMoveInput()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(horizontal, vertical);
        move = Vector2.ClampMagnitude(move, 1f);
        
        return move;
    }

    public void Movement()
    {
        Vector2 move = GetMoveInput();
        if (move != Vector2.zero)
        {
            float dt = Time.deltaTime;

            transform.Translate(move * speed * dt);
        }
    }
}
