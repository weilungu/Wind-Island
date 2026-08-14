using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    PlayerInput input;
    SpriteRenderer sprite;
    Animator anim;
    
    New_PlayerController player;
    PlayerMove move;
    PlayerDash dash;
    
    private static int MoveX = Animator.StringToHash("MoveX");
    private static int MoveY = Animator.StringToHash("MoveY");
    
    public override void Enter()
    {
        base.Enter();
        
        // Initialize Context
        input = ctx.Get<PlayerInput>();
        sprite = ctx.Get<SpriteRenderer>();
        anim = ctx.Get<Animator>();
            
        player = ctx.Get<New_PlayerController>();
        move = ctx.Get<PlayerMove>();
        dash = ctx.Get<PlayerDash>();
    }
    
    public override void LogicalUpdate()
    {
        if (!input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
     
        
        if (input.Dash && dash.canDash)
            stateMachine.SwitchState(typeof(PlayerState_Dash));
    }

    public override void PhysicalUpdate()
    {
        player.Flip(sprite);
        move.Move(player.MoveDirection);
        
        anim.SetFloat(MoveX, player.MoveDirection.x);
        anim.SetFloat(MoveY, player.MoveDirection.y);
    }
}
