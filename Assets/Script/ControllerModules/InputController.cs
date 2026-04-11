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
        
        movePressed = Input.GetKeyDown(KeyCode.W) ||
                      Input.GetKeyDown(KeyCode.A) ||
                      Input.GetKeyDown(KeyCode.S) ||
                      Input.GetKeyDown(KeyCode.D) ||
                      Input.GetKeyDown(KeyCode.UpArrow) ||
                      Input.GetKeyDown(KeyCode.DownArrow) ||
                      Input.GetKeyDown(KeyCode.LeftArrow) ||
                      Input.GetKeyDown(KeyCode.RightArrow);
    }
}