using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    MainMenuButton currBtn;
    List<MainMenuButton> buttons;
    
    [HideInInspector] public GameObject lastSelect;
    
    // === Unity Life Cycle === //
    void Awake()
    {
        buttons = new List<MainMenuButton>(
            GetComponentsInChildren<MainMenuButton>(true)
        );
    }

    void Start()
    {
        if (buttons.Count < 0) return;
        
        Select(buttons[0]);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null)
            EventSystem.current.SetSelectedGameObject(lastSelect);
    }
    
    // === Self API === //
    public void Select(MainMenuButton target)
    {
        if (currBtn == target) return;
        
        currBtn?.Deselect();
        
        currBtn = target;
        currBtn.Select();
        lastSelect = currBtn.gameObject;
        
        EventSystem.current.SetSelectedGameObject(currBtn.gameObject);
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
