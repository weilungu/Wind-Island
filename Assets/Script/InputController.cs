using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Input of Pressed
    public bool dashPressed { get; private set; }
    

    void Update()
    {
        dashPressed = Input.GetKey(KeyCode.LeftShift);
    }
}