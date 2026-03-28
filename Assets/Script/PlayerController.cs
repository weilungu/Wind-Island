using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Input")]
    [SerializeField] float speed; 
    
    InputController inp;

    private void Awake()
    {
        inp = GetComponent<InputController>();
    }


    #region Unity LifeCycle
    
        void Update()
        {
            Move();
            
            inp.GetDashInput();
        }
    
    #endregion

    
    
    #region Self Methods
    
        void Move()
        {
            Vector2 move = inp.GetMoveInput();
            if (move != Vector2.zero)
            {
                float dt = Time.deltaTime;

                // print($"horizontal: {inp.horizontal}");
                // print($"vertical: {inp.vertical}");

                transform.Translate(move * speed * dt);
            }
        }

    #endregion
}
