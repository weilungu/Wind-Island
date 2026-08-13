using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void LogicalUpdate()
    {
        if (ctx.Get<PlayerInput>().Move)
            stateMachine.SwitchState(typeof(PlayerState_Move));
        
        
        if (ctx.Get<PlayerPosture>().CurrPosture >= ctx.Get<PlayerPosture>().MaxPosture)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak));
    }

    public override void PhysicalUpdate()
    {
        ctx.Get<PlayerMove>().StopMove();
    }
}
