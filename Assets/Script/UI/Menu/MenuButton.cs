using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    IPointerEnterHandler,
    IPointerClickHandler
{
    MenuNavigation navigation;
    Button button;
    
    ButtonEffect effect;

    // === Unity Life Cycle === //
    private void Awake()
    {
        navigation = GetComponentInParent<MenuNavigation>();
        button = GetComponent<Button>();
        
        effect = GetComponent<ButtonEffect>();
    }

    
    // === Interface API === //
    public void OnSelect(BaseEventData eventData) => Select();
    
    public void OnDeselect(BaseEventData eventData) => Deselect();

    public void OnPointerEnter(PointerEventData eventData) => navigation.Select(this);
    
    public void OnPointerClick(PointerEventData eventData) => button.onClick.Invoke();
    
    
    // === Self API === //
    public void Select()
    {
        effect.SelectEffect();
        navigation.lastSelect = gameObject;
        
        // print($"Select: '{button.name}'");
    }
    public void Deselect()
    {
        if (EventSystem.current.currentSelectedGameObject is null)
            return;
        
        effect.DeselectEffect();
    }
}
