using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Inputs of Axis
    public float vertical; // 垂直
    public float horizontal; // 水平

    // Instance
    DashController dash;

    void Awake()
    {
        dash = GetComponent<DashController>();
    }

    #region Self Methods

        public Vector2 GetMoveInput()
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            Vector2 move = new Vector2(horizontal, vertical);
            move = Vector2.ClampMagnitude(move, 1f);

            return move;
        }
        
        public void GetDashInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dash.Dash();
                
                print("Dashing");
            }
        }

    #endregion
}