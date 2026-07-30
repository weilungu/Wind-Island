using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    List<MainMenuButton> buttons;
    
    MainMenu controller;
    MainMenuButtonEffect effect;

    private void Awake()
    {
        buttons = new List<MainMenuButton>(
            GetComponentsInChildren<MainMenuButton>()
        );
    }

    public void OnSelect(BaseEventData eventData)
    {
        controller.OnKeyboardInput();
    } // if keyboard selected button, do something

    public void OnDeselect(BaseEventData eventData)
    {
        effect.DeselectEffect();
    } // if keyboard selected button, do something

    public void OnPointerEnter(PointerEventData eventData)
    {
        effect.SelectEffect();
    } // if cursor in button range, do something

    public void OnPointerExit(PointerEventData eventData)
    {
        effect.DeselectEffect();
    }  // if cursor out button range, do something
}
