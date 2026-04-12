using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Input of Pressed
    public bool dashPressed { get; private set; }
    public bool movePressed { get; private set; }

    void Update()
    {
        dashPressed = Input.GetKeyDown(KeyCode.Space);
        
        movePressed = Input.GetKey(KeyCode.W) ||
                      Input.GetKey(KeyCode.A) ||
                      Input.GetKey(KeyCode.S) ||
                      Input.GetKey(KeyCode.D) ||
                      Input.GetKey(KeyCode.UpArrow) ||
                      Input.GetKey(KeyCode.DownArrow) ||
                      Input.GetKey(KeyCode.LeftArrow) ||
                      Input.GetKey(KeyCode.RightArrow);
    }
}