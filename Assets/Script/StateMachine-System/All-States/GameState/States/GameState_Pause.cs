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
        
        Time.timeScale = 0f;
        GM.player.DisableGameplay();
        
        Debug.Log("Entered Pause");
    }

    public override void LogicalUpdate()
    {
        if (input.OpenCloseMenu)
            stateMachine.SwitchState(typeof(GameState_InGame));
    }
}
