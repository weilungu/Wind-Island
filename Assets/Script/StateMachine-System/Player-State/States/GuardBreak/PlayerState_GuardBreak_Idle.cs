using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/GuardBreak_Idle",
    fileName = "PlayerState_GuardBreak_Idle")
]
public class PlayerState_GuardBreak_Idle : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        
        move.StopMove();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        guardBreak.SetGuardBreakBe(!guardBreak.Less_ThreshPCT);
        
        if (input.Move)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak_Move));
        
        
        if (!guardBreak.IsGuardBreak && !input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
    }
}
