using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        ctx.dash.TryDash();
    }

    public override void LogicalUpdate()
    {
        if (ctx.dash.isDashing) return;
        
        stateMachine.SwitchState(
            ctx.input.Move
            ? typeof(PlayerState_Move)
            : typeof(PlayerState_Idle)
        );
    }
}
