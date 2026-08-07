using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    public PlayerInput input;
    public SpriteRenderer sprite;
    public Animator anim;
    
    public New_PlayerController player;
    public PlayerMove move;

    public PlayerStateContext(
        PlayerInput input,
        SpriteRenderer sprite,
        Animator anim,
        
        New_PlayerController player,
        PlayerMove move)
    {
        this.input = input;
        this.sprite = sprite;
        this.anim = anim;
        
        this.player = player;
        this.move = move;
    }
}
