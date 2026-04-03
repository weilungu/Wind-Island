using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Input of Pressed
    public bool dashPressed { get; private set; }
    
    public bool moveUpPressed { get; private set; }
    public bool moveDownPressed { get; private set; }
    public bool moveLeftPressed { get; private set; }
    public bool moveRightPressed { get; private set; }


    void Update()
    {
        dashPressed = Input.GetKey(KeyCode.Space);

        moveUpPressed = Input.GetKey(KeyCode.W);
        moveDownPressed = Input.GetKey(KeyCode.S);
        moveLeftPressed = Input.GetKey(KeyCode.A);
        moveRightPressed = Input.GetKey(KeyCode.D);
    }
}