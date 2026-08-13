using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/Player_State/GuardBreak", fileName = "PlayerState_GuardBreak")]
public class PlayerState_GuardBreak : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        
        ctx.Get<PlayerPosture>().ResetPosture();
    }
}
