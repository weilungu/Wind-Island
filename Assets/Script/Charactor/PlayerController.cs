using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    float h;
    float v;
    
    // Instance
    InputController inp;
    
    MoveController move;
    DashController dash;
    
    [SerializeField] StateMachine fsm;
    
    private void Awake()
    {
        inp = GetComponent<InputController>();
        
        move = GetComponent<MoveController>();
        dash = GetComponent<DashController>();
    }

    private void Start()
    {
        fsm.SetGameState(GameState.Idle);
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        PlayerAction();
        
        print($"{h}, {v}");
    }
    
    
    // Self-Methods
    void PlayerAction()
    {
        switch (fsm.gameState)
        {
            case GameState.Idle:
                if (inp.movePressed)
                {
                    fsm.SetGameState(GameState.Move);
                }
                
                break;
        
            
            case GameState.Move:
                Vector2 dir = new Vector2(h, v).normalized;
                move.Movement(dir);
                
                if (dir.Equals(Vector2.zero))
                {
                    fsm.SetGameState(GameState.Idle);
                }
                else if (inp.dashPressed)
                {
                    dash.TryDash(dir);
                }
                
                break;
            
            
            case GameState.Dash:
                break;
        }
    }
}
