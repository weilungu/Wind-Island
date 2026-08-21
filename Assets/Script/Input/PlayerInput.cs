using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    
    // === Gameplay Action Map === //
    Vector2 axis => playerInputActions.Gamplay.Axis.ReadValue<Vector2>();
    
    
    // Axis Values
    public float Horizontal => axis.x;
    public float Vertical => axis.y;
    
    
    // is Pressed
    public bool Move => Horizontal != 0 || Vertical != 0;
    public bool Dash => playerInputActions.Gamplay.Dash.WasPressedThisFrame();
    public bool Attack => playerInputActions.Gamplay.Attack.WasPressedThisFrame();
    
    
    // Open Close Menu
    public bool OpenCloseMenu => playerInputActions.Menu.MenuOpenClose.WasPressedThisFrame();
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    
    
    // === Self API === //
    
    // Enable Input Actions
    public void EnableGameplay() => playerInputActions.Gamplay.Enable();
    public void EnableMenu() => playerInputActions.Menu.Enable();
    
    // Disable Input Actions
    public void DisableGameplay() => playerInputActions.Gamplay.Disable();
    public void DisableMenu() => playerInputActions.Menu.Disable();
}
