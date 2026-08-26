using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    List<MainMenuButton> buttons;
    MainMenuButton currentButton;
    GameObject lastSelected;

    // === Unity Life Cycle === //
    void Awake()
    {
        buttons = new List<MainMenuButton>(
            GetComponentsInChildren<MainMenuButton>(true)
        );
    }

    void Start()
    {
        if (buttons.Count == 0) return;

        Select(buttons[0]);
    }

    void Update()
    {
        if (EventSystem.current is null) return;

        GameObject currentSelected =
            EventSystem.current.currentSelectedGameObject;

        if (currentSelected is null)
        {
            if (lastSelected is null)
                EventSystem.current.SetSelectedGameObject(lastSelected);

            return;
        }

        MainMenuButton button =
            currentSelected.GetComponent<MainMenuButton>();

        if (button is null || button == currentButton) return;

        Select(button);
    }

    // === Selection API === //
    public void Select(MainMenuButton target)
    {
        if (target is null || target == currentButton) return;

        currentButton?.Deselect();

        currentButton = target;
        currentButton.Select();

        lastSelected = currentButton.gameObject;

        if (EventSystem.current is not null)
            EventSystem.current.SetSelectedGameObject(
                currentButton.gameObject
            );
    }
}
