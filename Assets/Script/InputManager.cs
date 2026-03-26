using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    
    // Unity Life Cycle
    private void Update()
    {
        Moves();
    }

    void Moves()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        float dt = Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            print("A");
            transform.Translate(Vector2.left * speed * dt);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * dt);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * dt);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * dt);
        }
    }
}
