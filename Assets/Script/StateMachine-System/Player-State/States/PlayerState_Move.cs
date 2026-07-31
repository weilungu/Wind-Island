using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    
    public override void Enter()
    {
        // ctx.anim.Play("Move_Hori");
        ctx.anim.Play("Move");
        Debug.Log("Move");
    }

    public override void LogicalUpdate()
    {
        if ( !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) )
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicalUpdate()
    {
        // ctx.anim.SetFloat();
    }
}
