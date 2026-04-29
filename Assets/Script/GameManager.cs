using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack,
    Dead,
}
public enum EnemyState
{
    Idle,
    Chase,
    Dash,
    Attack,
    Dead,
}
public enum GameState
{
    InGame,
    Paused,
    GameOver,
    Quit,
}

public class GameManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private Image pausePanel;

    [Header("Instance")]
    [SerializeField] private InputController inp;
    // [SerializeField] private StateMachine fsm;

    [Header("States Cells")] 
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController[] enemies;
    
    // State
    GameState gameState;

    private void Start()
    {
        SetGameState(GameState.InGame);
    }
    private void Update()
    {
        GameActionState();
    }
    private void FixedUpdate()
    {
        GamePhysicsState();
    }


    // State Layer
    void SetGameState(GameState state)
    {
        gameState = state;
    }
    void GameActionState()
    {
        switch (gameState)
        {
            case GameState.InGame:

                HideCursor(true);
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                
                player.ActionState();

                foreach (EnemyController e in enemies)
                {
                    e.ActionState();
                }

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(true);
                    SetGameState(GameState.Paused);
                }
                break;


            case GameState.Paused:
                HideCursor(false);
                Time.timeScale = 0;

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(false);
                    SetGameState(GameState.InGame);
                }
                break;


            case GameState.Quit:
                QuitGame();
                break;
        }
    }
    void GamePhysicsState()
    {
        switch (gameState)
        {
            case GameState.InGame:
                player.PhysicsState();

                foreach (EnemyController e in enemies)
                {
                    e.PhysicsState();
                }
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
    
    // With UI
    public void GameStart()
    {
        print("GameStart");
        SceneManager.LoadScene("Game");
    }
    public void ReturnToMainMenu()
    {
        print("ReturnToMainMenu");
        SceneManager.LoadScene("GameDirectory");
    }
    public void QuitGame()
    {
        print("Quit Game");
        Application.Quit();
    }
}