using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    protected override bool CanAttack => true;

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        if (input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Move));
    }

    public override void PhysicalUpdate()
    {
        move.StopMove();
    }
}
