using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
