using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    float vertical;
    float horizontal;
    
    Vector2 move;
    
    [SerializeField] float speed;

    void GetMoveInput()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        move = new Vector2(horizontal, vertical);
        move = Vector2.ClampMagnitude(move, 1f);
    }

    public void Movement()
    {
        if (move != Vector2.zero)
        {
            float dt = Time.deltaTime;

            transform.Translate(move * speed * dt);
        }
    }
}
