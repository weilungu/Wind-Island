using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Inputs of Axis
    public float vertical; // 垂直
    public float horizontal; // 水平
    
    // [SerializeField] float speed = 5;
    
    // Unity Life Cycle
    private void Update()
    {
        Moves();
    }

    void Moves()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }
}
