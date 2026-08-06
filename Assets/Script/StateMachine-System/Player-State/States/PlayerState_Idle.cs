using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        // ctx.anim.Play("Idle_Hori");
        ctx.anim.Play("Idle");
        Debug.Log("Idle");
    }

    public override void LogicalUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }
    }
}
