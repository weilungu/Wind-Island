using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Data/StateMachine/Player_State/Attack/Normal",
    fileName = "PlayerState_Attack_Normal")
]
public class PlayerState_Attack_Normal : PlayerState
{
    AnimatorStateInfo stateInfo;
    
    public override void Enter()
    {
        base.Enter();
        
        combat.Attack();
        combat.AdvanceStep();
    }

    public override void LogicalUpdate()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
        if (stateInfo.normalizedTime >= 1f)
            stateMachine.SwitchState(
                input.Move
                ? typeof(PlayerState_Move)
                : typeof(PlayerState_Idle)
            );
        else 
            move.Move();
    }

    public override void Exit()
    {
        if (!combat.CanAttack)
            combat.Reset_AfterDelay(combat.AttackCooldown);
    }
}
