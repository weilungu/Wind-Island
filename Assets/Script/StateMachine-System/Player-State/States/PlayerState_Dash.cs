using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    [Header("Dash Posture")]
    [SerializeField] int postureAmount = 10;
    
    public override void Enter()
    {
        base.Enter();
        
        ctx.Get<PlayerDash>().TryDash();
        ctx.Get<PlayerPosture>().TakePosture(postureAmount);
    }

    public override void LogicalUpdate()
    {
        if (ctx.Get<PlayerDash>().isDashing) return;
        
        stateMachine.SwitchState(
            ctx.Get<PlayerInput>().Move
            ? typeof(PlayerState_Move)
            : typeof(PlayerState_Idle)
        );
        
        
        if (ctx.Get<PlayerPosture>().CurrPosture >= ctx.Get<PlayerPosture>().MaxPosture)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak));
    }
}
