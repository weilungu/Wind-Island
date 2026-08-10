using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        ctx.Get<PlayerDash>().TryDash();
    }

    public override void LogicalUpdate()
    {
        if (ctx.Get<PlayerDash>().isDashing) return;
        
        stateMachine.SwitchState(
            ctx.Get<PlayerInput>().Move
            ? typeof(PlayerState_Move)
            : typeof(PlayerState_Idle)
        );
    }
}
