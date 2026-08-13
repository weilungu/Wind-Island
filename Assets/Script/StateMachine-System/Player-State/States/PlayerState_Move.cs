using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    private static int MoveX = Animator.StringToHash("MoveX");
    private static int MoveY = Animator.StringToHash("MoveY");

    public override void LogicalUpdate()
    {
        if (!ctx.Get<PlayerInput>().Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
     
        
        if (ctx.Get<PlayerInput>().Dash && ctx.Get<PlayerDash>().canDash)
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        
        
        if (ctx.Get<PlayerPosture>().CurrPosture >= ctx.Get<PlayerPosture>().MaxPosture)
            stateMachine.SwitchState(typeof(PlayerState_GuardBreak));
    }

    public override void PhysicalUpdate()
    {
        New_PlayerController player = ctx.Get<New_PlayerController>();
        PlayerMove move = ctx.Get<PlayerMove>();
        SpriteRenderer sprite = ctx.Get<SpriteRenderer>();
        Animator anim = ctx.Get<Animator>();

        player.Flip(sprite);
        move.Move(player.MoveDirection);
        
        anim.SetFloat(MoveX, player.MoveDirection.x);
        anim.SetFloat(MoveY, player.MoveDirection.y);
    }
}
