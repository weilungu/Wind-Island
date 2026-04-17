using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimParams
{
    public static readonly int Move = Animator.StringToHash("Move");
    public static readonly int MoveUp = Animator.StringToHash("MoveUp");
    public static readonly int MoveDown = Animator.StringToHash("MoveDown");
    
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int IdleUp = Animator.StringToHash("IdleUp");
    public static readonly int IdleDown = Animator.StringToHash("IdleDown");
    
    public static readonly int Attack = Animator.StringToHash("Attack");
    
}
