using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// All State Enum
public enum Player_State
{
    Idle,
    
    Move,
    Dash,
    Attack,
    
    GuardBreak,
    HitStun,
    
    Dead,
}
public enum Enemy_State
{
    Idle,
    
    Chase,
    Dash,
    Attack,
    
    GuardBreak,
    HitStun,
    
    Dead,
}
public enum Game_State
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
    Game_State gameState;

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
        SetGameState(Game_State.InGame);
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
    void SetGameState(Game_State state)
    {
        gameState = state;
    }
    void GameActionState()
    {
        switch (gameState)
        {
            case Game_State.InGame:

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
                    SetGameState(Game_State.Paused);
                }
                break;


            case Game_State.Paused:
                HideCursor(false);
                Time.timeScale = 0;
                AudioListener.pause = true;

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(false);
                    SetGameState(Game_State.InGame);
                }
                break;
            
            
            case Game_State.GameOver:
                if (player.IsDead)
                {
                    HideCursor(false);
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    
                    gameOverPanel.gameObject.SetActive(true);
                }
                break;


            case Game_State.Quit:
                QuitGame();
                break;
        }
    }
    void GamePhysicsState()
    {
        switch (gameState)
        {
            case Game_State.InGame:
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
        SetGameState(Game_State.GameOver);
    }
}