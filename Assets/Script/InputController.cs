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
    [SerializeField] DashController dash;
    
    
    #region Self Methods

        public Vector2 GetMoveInput()
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            Vector2 move = new Vector2(horizontal, vertical);
            move = Vector2.ClampMagnitude(move, 1f);

            return move;
        }
        public void Dash()
        {
            if (dash.isDashing)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && dash.canDash)
            {
                print("Dashing");
                StartCoroutine(dash.DashCoroutine());
            }
        }

    #endregion
}