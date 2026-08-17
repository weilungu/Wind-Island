using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    protected override bool CanBe_Attack => false;

    public override void Enter()
    {
        base.Enter();
        
        // Enter Logic
        dash.TryDash();
        
        posture.TakePosture(dash.GetPosture());
        posture.ResetTimeGap();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
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
