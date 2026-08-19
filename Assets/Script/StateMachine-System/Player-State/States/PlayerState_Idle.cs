using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/Idle",
    fileName = "PlayerState_Idle")
]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        
        move.StopMove();
    }

    public override void LogicalUpdate()
    {
        if (input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Move));
        
        else if (combat.CanAttack && input.Attack)
            stateMachine.SwitchState(typeof(PlayerState_Attack_Normal));
    }
}
