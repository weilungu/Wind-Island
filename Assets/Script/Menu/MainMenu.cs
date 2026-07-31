using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum InputMode
{
    Mouse,
    Keyboard
}

public class MainMenu : MonoBehaviour
{
    InputMode inputMode;
    
    MainMenuButton currBtn;
    List<MainMenuButton> buttons;
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        currBtn = GetComponentInChildren<MainMenuButton>();
        
        buttons = new List<MainMenuButton>(
            GetComponentsInChildren<MainMenuButton>(true)
        );
    }

    
    // === Self API === //
    public void Select(MainMenuButton tg)
    {
        
    }

    public void Deselect()
    {
        
    }
    public void SetInputMode(InputMode mode)
    {
        
    }

    public void OnKeyboardInput()
    {
        
    }

    
    // === UI API === //
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
        print("Quit Game");
    }
}
