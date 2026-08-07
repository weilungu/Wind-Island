using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerStateContext ctx;
    
    public void Initialize(PlayerStateMachine stateMachine, PlayerStateContext ctx)
    {
        this.stateMachine = stateMachine;
        this.ctx = ctx;
    }
    
        
    public virtual void Enter() {}

    public virtual void Exit() {}

    public virtual void LogicalUpdate() {}

    public virtual void PhysicalUpdate() {}
}
