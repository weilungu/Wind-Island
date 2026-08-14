using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    PlayerDash dash;
    PlayerPosture posture;
    PlayerInput input;
    
    [Header("Dash Posture")]
    [SerializeField] int postureAmount = 10;
    
    public override void Enter()
    {
        base.Enter();
        
        // Initialize Context
        dash = ctx.Get<PlayerDash>();
        posture = ctx.Get<PlayerPosture>();
        input = ctx.Get<PlayerInput>();
        
        // Enter Logic
        dash.TryDash();
        posture.TakePosture(postureAmount);
    }

    public override void LogicalUpdate()
    {
        if (dash.isDashing) return;
        
        stateMachine.SwitchState(
            input.Move
            ? typeof(PlayerState_Move)
            : typeof(PlayerState_Idle)
        );
        
        
        if (posture.CurrPosture >= posture.MaxPosture)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak));
    }
}
