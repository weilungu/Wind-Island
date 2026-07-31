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

    // === Unity Life Cycle === //
    private void Awake()
    {
        controller = GetComponentInParent<MainMenu>();
        effect = GetComponent<MainMenuButtonEffect>();
        button = GetComponent<Button>();
    }

    
    // === Interface API === //
    public void OnSelect(BaseEventData eventData) => Select();
    
    public void OnDeselect(BaseEventData eventData) => Deselect();

    public void OnPointerEnter(PointerEventData eventData) => controller.Select(this);
    
    public void OnPointerClick(PointerEventData eventData) => button.onClick.Invoke();
    
    
    // === Self API === //
    public void Select()
    {
        effect.SelectEffect();
        controller.lastSelect = gameObject;
    }
    public void Deselect()
    {
        if (EventSystem.current.currentSelectedGameObject is null) return;
        
        effect.DeselectEffect();
    }
}
