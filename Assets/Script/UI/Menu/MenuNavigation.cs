using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    MenuButton currBtn;
    List<MenuButton> buttons;
    
    [HideInInspector] public GameObject lastSelect;
    
    void Awake()
    {
        buttons = new List<MenuButton>(
            GetComponentsInChildren<MenuButton>(true)
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
    
    
    // === API === //
    public void Select(MenuButton target)
    {
        if (currBtn == target) return;
        
        currBtn?.Deselect();
        
        currBtn = target;
        currBtn.Select();
        lastSelect = currBtn.gameObject;
        
        EventSystem.current.SetSelectedGameObject(currBtn.gameObject);
    }
}
