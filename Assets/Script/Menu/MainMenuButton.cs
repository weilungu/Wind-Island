using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    IPointerEnterHandler,
    IPointerClickHandler
{
    MainMenu controller;
    
    MainMenuButtonEffect effect;
    Button button;

    private void Awake()
    {
        
    }

    // === Interface API === //
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
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    } // if click button, do something
}
