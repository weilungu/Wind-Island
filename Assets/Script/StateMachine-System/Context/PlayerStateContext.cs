using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    public PlayerInput input;
    public Animator anim;
    public New_PlayerController player;
    
    public PlayerMove move;

    public PlayerStateContext(
        PlayerInput input,
        Animator anim,
        New_PlayerController player,
        
        PlayerMove move)
    {
        this.input = input;
        this.anim = anim;
        this.player = player;
        
        this.move = move;
    }
}
