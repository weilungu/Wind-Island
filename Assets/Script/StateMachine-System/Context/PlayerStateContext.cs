using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    public PlayerInput input;
    public Animator anim;

    public PlayerStateContext(
        PlayerInput input,
        Animator anim)
    {
        this.input = input;
        this.anim = anim;
    }
}
