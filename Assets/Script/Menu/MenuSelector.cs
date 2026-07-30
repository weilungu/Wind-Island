using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] GameObject defaultSelect;
    
    GameObject lastSelect;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelect);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is not null)
            lastSelect = EventSystem.current.currentSelectedGameObject;
        
        
        if (EventSystem.current.currentSelectedGameObject is null)
            EventSystem.current.SetSelectedGameObject(lastSelect);
    }
}
