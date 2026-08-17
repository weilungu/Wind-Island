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
        
        guardBreak.SetIsGuardBreak(true);
        move.StopMove();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        if (input.Move)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak_Move));
    }
}
