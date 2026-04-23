using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panel")] [SerializeField] Image pausePanel;

    [Header("")] [SerializeField] InputController inp;
    [SerializeField] StateMachine fsm;

    [Header("States Cells")] [SerializeField]
    PlayerController player;

    [SerializeField] EnemyController enemy;

    void Start()
    {
        fsm.SetGameState(GameState.InGame);
    }

    void Update()
    {
        GameActionState();
    }

    void FixedUpdate()
    {
        GamePhysicsState();
    }


    void GameActionState()
    {
        switch (fsm.gameState)
        {
            case GameState.InGame:
                print("InGame");

                HideCursor(true);
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                
                player.ActionState();
                enemy.ActionState();

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(true);
                    fsm.SetGameState(GameState.Paused);
                }
                break;


            case GameState.Paused:
                HideCursor(false);
                Time.timeScale = 0;

                if (inp.escapePressed)
                {
                    pausePanel.gameObject.SetActive(false);
                    fsm.SetGameState(GameState.InGame);
                }
                break;


            case GameState.GameOver:
                print("GameOver");
                ExitGame();
                break;
        }
    }

    void GamePhysicsState()
    {
        switch (fsm.gameState)
        {
            case GameState.InGame:
                player.PhysicsState();
                enemy.PhysicsState();
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

    public void ExitGame()
    {
        Application.Quit();
    }
}