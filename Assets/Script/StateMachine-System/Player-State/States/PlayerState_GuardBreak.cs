using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/GuardBreak", fileName = "PlayerState_GuardBreak")]
public class PlayerState_GuardBreak : PlayerState
{
    PlayerGuardBreak guardBreak;
    PlayerMove move;
    
    public override void Enter()
    {
        base.Enter();
        
        // Initialize Context
        guardBreak = ctx.Get<PlayerGuardBreak>();
        move = ctx.Get<PlayerMove>();
        
        // Enter Logic
        ctx.Get<PlayerGuardBreak>().SetActive(true);
        ctx.Get<PlayerMove>().StopMove();
    }

    public override void PhysicalUpdate()
    {
        ctx.Get<PlayerMove>().Move(ctx.Get<New_PlayerController>().MoveDirection);
    }
}
