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
    
    public void Movement()
    {
        Vector2 move = inp.moveDirection;
        if (move != Vector2.zero)
        {
            float dt = Time.deltaTime;

            transform.Translate(move * speed * dt);
        }
    }
}