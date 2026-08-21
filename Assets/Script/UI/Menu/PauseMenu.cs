using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Awake Values
    PlayerInput input;
    
    // Inspector Values
    [SerializeField] GameObject pauseMenu;
    
    // Private Values
    bool isPaused = false;
    
    // === Unity Life Cycle === //
    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        input.EnableMenu();
    }

    void Update()
    {
        if (!input.OpenCloseMenu) return;
        
        SetPause(!isPaused);
    }
    
    
    // === Self Method === //
    void SetPause(bool value)
    {
        isPaused = value;
        
        pauseMenu.SetActive(value);
        Time.timeScale = value ? 0f : 1f;
    }
}
