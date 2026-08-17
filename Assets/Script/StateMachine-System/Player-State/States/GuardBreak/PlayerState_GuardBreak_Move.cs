using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/GuardBreak_Move",
    fileName = "PlayerState_GuardBreak_Move")
]
public class PlayerState_GuardBreak_Move : PlayerState
{
    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        guardBreak.SetGuardBreakBe(!guardBreak.Less_ThreshPCT);
        
        if (!input.Move)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak_Idle));
        
        
        if (!guardBreak.IsGuardBreak && input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Move));
    }

    public override void PhysicalUpdate()
    {
        player.Flip(sprite);
        move.GuardBreakMove();
        
        move.MoveAnimation();
    }
}
