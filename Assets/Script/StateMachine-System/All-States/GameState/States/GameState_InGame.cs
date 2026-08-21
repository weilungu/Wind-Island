using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Game_State/InGame",
    fileName = "GameState_InGame")
]
public class GameState_InGame : GameState
{
    public override void Enter()
    {
        base.Enter();
        
        Time.timeScale = 1f;
        GM.player.EnableGameplay();
        input.EnableMenu();
        
        Debug.Log("Entered InGame");
    }

    public override void LogicalUpdate()
    {
        if (input.OpenCloseMenu)
            stateMachine.SwitchState(typeof(GameState_Pause));
    }
}
