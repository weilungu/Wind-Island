using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Instant
    InputController inp;

    [SerializeField] float speed;

    private void Awake()
    {
        inp = GetComponent<InputController>();
    }

    public Vector2 GetMoveInput()
    {
        int vertical=0, horizontal=0;
        if (inp.moveUpPressed || inp.moveDownPressed)
        {
            vertical = inp.moveUpPressed ? 1 : -1;
        }
        if (inp.moveLeftPressed || inp.moveRightPressed)
        {
            horizontal = inp.moveRightPressed ? 1 : -1;
        }
        
        return new Vector2(horizontal, vertical);
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