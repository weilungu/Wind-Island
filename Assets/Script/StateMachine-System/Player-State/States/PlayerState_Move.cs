using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    [Header("Input Name")]
    [SerializeField] string XInputName = "MoveX";
    [SerializeField] string YInputName = "MoveY";
    
    private static int MoveX;
    private static int MoveY;

    public override void Enter()
    {
        base.Enter();
        
        MoveX = Animator.StringToHash(XInputName);
        MoveY = Animator.StringToHash(YInputName);
    }

    public override void LogicalUpdate()
    {
        if (!ctx.input.Move)
            stateMachine.SwitchState(typeof(PlayerState_Idle));
     
        if (ctx.input.Dash && ctx.dash.canDash)
            stateMachine.SwitchState(typeof(PlayerState_Dash));
    }

    public override void PhysicalUpdate()
    {
        ctx.player.Flip(ctx.sprite);
        ctx.move.Move(ctx.player.MoveDirection);
        
        ctx.anim.SetFloat(MoveX, ctx.player.MoveDirection.x);
        ctx.anim.SetFloat(MoveY, ctx.player.MoveDirection.y);
    }
}
