using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    private List<MainMenuButton> buttons;
    private MainMenuButton currentButton;
    private GameObject lastSelected;

    // === Unity Life Cycle === //
    private void Awake()
    {
        buttons = new List<MainMenuButton>(
            GetComponentsInChildren<MainMenuButton>(true)
        );
    }

    private void Start()
    {
        if (buttons.Count == 0) return;

        Select(buttons[0]);
    }

    private void Update()
    {
        if (EventSystem.current == null) return;

        GameObject currentSelected =
            EventSystem.current.currentSelectedGameObject;

        if (currentSelected == null)
        {
            if (lastSelected != null)
                EventSystem.current.SetSelectedGameObject(lastSelected);

            return;
        }

        MainMenuButton button =
            currentSelected.GetComponent<MainMenuButton>();

        if (button == null || button == currentButton) return;

        Select(button);
    }

    // === Selection API === //
    public void Select(MainMenuButton target)
    {
        if (target == null || target == currentButton) return;

        currentButton?.Deselect();

        currentButton = target;
        currentButton.Select();

        lastSelected = currentButton.gameObject;

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(
                currentButton.gameObject
            );
    }
}
