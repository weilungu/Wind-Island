using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameStart()
    {
        print("GameStart");
        SceneManager.LoadScene("InGame");
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