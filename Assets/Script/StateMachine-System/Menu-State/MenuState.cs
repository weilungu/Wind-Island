using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : ScriptableObject, IState
{
    Menu_StateMachine stateMachine;

    public void Initialize(Menu_StateMachine stateMachine)
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
