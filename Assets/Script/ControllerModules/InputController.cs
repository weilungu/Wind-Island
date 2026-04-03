using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector2 moveDirection { get; private set; }
    
    // Input of Pressed
    public bool dashPressed { get; private set; }
    public bool movePressed { get; private set; }

    void Update()
    {
        dashPressed = Input.GetKey(KeyCode.Space);
        movePressed = MovePressed();
    }

    bool MovePressed()
    {
        int dir_vertical=0, dir_horizontal=0;

        bool[] v_Pressed = { Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S) };
        bool[] h_Pressed = { Input.GetKey(KeyCode.D), Input.GetKey(KeyCode.A) };

        bool v_movePressed = v_Pressed[0] || v_Pressed[1];
        bool h_movePressed = h_Pressed[0] || h_Pressed[1];


        if (v_movePressed)
        {
            dir_vertical = v_Pressed[0] ? 1 : -1;
        }

        if (h_movePressed)
        {
            dir_horizontal = h_Pressed[0] ? 1 : -1;
        }

        moveDirection = new Vector2(dir_horizontal, dir_vertical);

        return v_movePressed || h_movePressed;
    }
}