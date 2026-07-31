using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : ScriptableObject, IState
{
    protected PlayerStateMachine stateMachine;

    public void Initialize(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
        
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicalUpdate()
    {
        
    }

    public virtual void PhysicalUpdate()
    {
        
    }
}
