using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : Player_State
{
    public override void Enter()
    {
        Debug.Log("PlayerState_Move");
    }

    public override void LogicalUpdate()
    {
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) )
        {
            stateMachine.SwitchState(stateMachine.idle);
        }
    }
}
