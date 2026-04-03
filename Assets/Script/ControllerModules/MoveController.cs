using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Instant
    InputController inp;

    [SerializeField] float speed;

    public Vector2 GetMoveInput()
    {
        // vertical = Input.GetAxisRaw("Vertical");
        // horizontal = Input.GetAxisRaw("Horizontal");

        // Vector2 move = new Vector2(horizontal, vertical);
        // move = Vector2.ClampMagnitude(move, 1f);

        return Vector2.zero;
    }

    public void Movement()
    {
        Vector2 move = inp.GetMoveInput();
        if (move != Vector2.zero)
        {
            float dt = Time.deltaTime;

            transform.Translate(move * speed * dt);
        }
    }
}