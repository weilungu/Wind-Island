using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/Move",
    fileName = "PlayerState_Move"
)]
public class PlayerState_Move : PlayerState
{
    public override void LogicalUpdate()
    {
        if (move.Direction != Vector2.zero)
            move.FacingDirection = move.Direction;
        
        combat.SetAttackPoint(move.FacingDirection);
        
        
        // transition
        if (!input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        
        else if (dash.canDash && input.Dash)
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        
        else if (combat.CanAttack && input.Attack)
            stateMachine.SwitchState(typeof(PlayerState_Attack_Normal));
    }

    public override void PhysicalUpdate()
    {
        player.Flip(sprite);
        move.Move();
        
        move.MoveAnimation();
    }
}
