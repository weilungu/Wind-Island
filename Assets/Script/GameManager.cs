using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// All State Enum
public enum PlayerState
{
    Idle,
    
    Move,
    Dash,
    Attack,
    
    GuardBreak,
    HitStun,
    
    Dead,
}
public enum EnemyState
{
    Idle,
    
    Chase,
    Dash,
    Attack,
    
    GuardBreak,
    HitStun,
    
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
    [SerializeField] private Image gameOverPanel;

    [Header("Field Instance")]
    [SerializeField] private InputController inp;

    [Header("States Cells")] 
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController[] enemies;
    
    // State
    GameState gameState;

    private void OnEnable()
    {
        if (player is not null)
            player.OnPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        if (player is not null)
            player.OnPlayerDead -= HandlePlayerDead;
    }

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
                AudioListener.pause = false;
                
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
                AudioListener.pause = true;

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(false);
                    SetGameState(GameState.InGame);
                }
                break;
            
            
            case GameState.GameOver:
                if (player.IsDead)
                {
                    HideCursor(false);
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    
                    gameOverPanel.gameObject.SetActive(true);
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
    public void RetryGame()
    {
        print("RetryGame");
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        print("Quit Game");
        Application.Quit();
    }

    void HandlePlayerDead()
    {
        SetGameState(GameState.GameOver);
    }
}