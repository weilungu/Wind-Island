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
    MainMenu currBtn;
    List<MainMenuButton> buttons;
    
    // === Unity Life Cycle === //
    void Awake()
    {
        currBtn = GetComponentInChildren<MainMenu>();
    }

    // === Self API === //
    public void Select(MainMenuButton tg)
    {
        
    }
    public void OnKeyboardInput()
    {
        
    }
    public void SetInputMode(InputMode mode)
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
