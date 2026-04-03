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

    public Vector2 GetMoveInput()
    {
        int vertical=0, horizontal=0;
        if (moveUpPressed || moveDownPressed)
        {
            vertical = moveUpPressed ? 1 : -1;
        }
        else if (moveLeftPressed || moveRightPressed)
        {
            horizontal = moveRightPressed ? 1 : -1;
        }
        
        return new Vector2(horizontal, vertical);
    }
}