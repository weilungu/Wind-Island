using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string animationName;
    int animationHash;
    
    protected PlayerStateMachine stateMachine;
    protected PlayerStateContext ctx;

    void OnEnable()
    {
        animationHash = Animator.StringToHash(animationName);
    }
    
    public void Initialize(PlayerStateMachine stateMachine, PlayerStateContext ctx)
    {
        this.stateMachine = stateMachine;
        this.ctx = ctx;
    }
    
    public virtual void Enter()
    {
        ctx.Get<Animator>().Play(animationHash);
        Debug.Log(animationName);
    }

    public virtual void Exit() {}

    public virtual void LogicalUpdate() {}

    public virtual void PhysicalUpdate() {}
}
