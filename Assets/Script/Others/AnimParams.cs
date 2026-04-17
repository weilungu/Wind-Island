using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimParams
{
    
    public static readonly int Attack = Animator.StringToHash("Attack");
    
    public static readonly int MoveX = Animator.StringToHash("MoveX");
    public static readonly int MoveY = Animator.StringToHash("MoveY");
    public static readonly int IsMoving = Animator.StringToHash("IsMoving");
}
