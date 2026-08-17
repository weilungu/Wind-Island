using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/Dash",
    fileName = "PlayerState_Dash")
]
public class PlayerState_Dash : PlayerState
{
    protected override bool CanBe_Attack => false;

    public override void Enter()
    {
        base.Enter();
        
        dash.TryDash();
        posture.TakePosture(dash.GetPosture());
        posture.ResetTimeGap();
        
        guardBreak.SetGuardBreakBe(posture.CurrPosture >= posture.MaxPosture);
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        if (dash.isDashing) return;
        
        
        if (guardBreak.IsGuardBreak)
        {
            stateMachine.SwitchState(
                input.Move
                ? typeof(PlayerState_GuardBreak_Move)
                : typeof(PlayerState_GuardBreak_Idle)
            );
            
            return;
        }
        
        stateMachine.SwitchState(
            input.Move
            ? typeof(PlayerState_Move)
            : typeof(PlayerState_Idle)
        );
    }
}
