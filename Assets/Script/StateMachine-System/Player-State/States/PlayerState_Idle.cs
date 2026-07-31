using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : Player_State
{
    public override void Enter()
    {
        Debug.Log("PlayerState_Idle");
    }

    public override void LogicalUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }
    }
}
