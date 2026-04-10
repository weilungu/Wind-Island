using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float speed;
    
    public void Movement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float dt = Time.deltaTime;

            transform.Translate(direction * speed * dt);
        }
    }
}