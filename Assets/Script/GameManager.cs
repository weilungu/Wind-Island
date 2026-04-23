using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] Image pausePanel;
    
    [Header("")]
    [SerializeField] InputController inp;
    [SerializeField] StateMachine fsm;
    
    [Header("States Cells")]
    [SerializeField] PlayerController player;
    [SerializeField] EnemyController enemy;
    
    void Start()
    {
        fsm.SetGameState(GameState.InGame);
    }
    void Update()
    {
        GameActionState();
    }

    
    void GameActionState()
    {
        switch (fsm.gameState)
        {
            case GameState.InGame:
                print("InGame");
                
                HideCursor(true);
                pausePanel.gameObject.SetActive(false);
                
                if (inp.escapePressed) fsm.SetGameState(GameState.Paused);
                break;
            
            
            case GameState.Paused:
                print("Paused");
                HideCursor(false);
                GamePause();
                break;
            
            
            case GameState.GameOver:
                print("GameOver");
                ExitGame();
                break;
        }
    }
    
    void HideCursor(bool isHide)
    {
        Cursor.visible = !isHide;

        Cursor.lockState = isHide
            ? CursorLockMode.Locked
            : CursorLockMode.None;
    }
    void GamePause()
    {
        pausePanel.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
