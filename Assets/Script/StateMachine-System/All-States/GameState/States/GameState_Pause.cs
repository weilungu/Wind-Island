using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Game_State/Pause",
    fileName = "GameState_Pause")
]
public class GameState_Pause : GameState
{
    public override void Enter()
    {
        base.Enter();
        
        pause.SetPause(true);
        
        Debug.Log("Entered Pause");
    }

    public override void LogicalUpdate()
    {
        if (input.OpenCloseMenu)
            stateMachine.SwitchState(typeof(GameState_InGame));
    }
}
