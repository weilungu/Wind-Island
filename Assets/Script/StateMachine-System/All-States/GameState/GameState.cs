using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : ScriptableObject, IState
{
    protected GameStateMachine stateMachine;
    protected GameStateContext ctx;
    
    // Contexts
    
    
    public void Initialize(GameStateMachine stateMachine, GameStateContext ctx)
    {
        this.stateMachine = stateMachine;
        this.ctx = ctx;
    }
    
    public virtual void Enter()
    {
        // Context
        GetContexts();
    }

    public virtual void Exit() {}

    public virtual void LogicalUpdate() {}

    public virtual void PhysicalUpdate() {}

    
    void GetContexts()
    {
        
    }
}
