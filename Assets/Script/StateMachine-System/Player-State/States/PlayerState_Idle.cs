using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    PlayerInput input;
    PlayerMove move;

    public override void Enter()
    {
        base.Enter();
        
        // Initialize Context
        input = ctx.Get<PlayerInput>();
        move = ctx.Get<PlayerMove>();
    }
    
    public override void LogicalUpdate()
    {
        if (input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Move));
    }

    public override void PhysicalUpdate()
    {
        move.StopMove();
    }
}
