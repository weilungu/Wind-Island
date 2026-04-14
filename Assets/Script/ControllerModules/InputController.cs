using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Input of Pressed
    public bool dashPressed { get; private set; }
    public bool movePressed { get; private set; }
    public bool attackPressed { get; private set; }

    void Update()
    {
        dashPressed = Input.GetKeyDown(KeyCode.Space);
        
        movePressed = Input.GetButtonDown("Vertical") || Input.GetButtonUp("Vertical") ||
                      Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal");

        attackPressed = Input.GetMouseButtonDown(0); // 左鍵
    }
}