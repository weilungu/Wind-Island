using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    // Awake Values
    PlayerInput input;
    
    [Header("Inspector Values")]
    [SerializeField] New_PlayerController player;
    [SerializeField] GameObject pauseMenu;
    
    
    // === Unity Life Cycle === //
    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        input.EnableMenu();
    }
    
    // === Self Method === //
    public void SetPause(bool enable)
    {
        pauseMenu.SetActive(enable);
        Time.timeScale = enable ? 0f : 1f;
        
        player.GameplaySetActive(!enable);
    }
}
