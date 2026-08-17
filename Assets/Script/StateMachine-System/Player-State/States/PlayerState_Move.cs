using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        if (!input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
     
        
        if (input.Dash && dash.canDash)
            stateMachine.SwitchState(typeof(PlayerState_Dash));
    }

    public override void PhysicalUpdate()
    {
        player.Flip(sprite);
        move.Movement();
        
        move.PlayMoveAnimation();
    }
}
