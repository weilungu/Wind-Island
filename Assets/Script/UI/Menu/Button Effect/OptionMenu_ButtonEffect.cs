using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu_ButtonEffect : ButtonEffect
{
    // Awake Values
    OptionButtons prefabs;
    
    // Private Values
    string currentName;

    
    // === Life Cycle === //
    void Awake()
    {
        prefabs = GetComponentInParent<OptionButtons>(true);
        
        currentName = gameObject.name;
    }

    
    // === Abstract Method Override === //
    public override void SelectEffect()
    {
        CompareName(prefabs.Audio_Pressed_Button.name, P_A);
    }

    public override void DeselectEffect()
    {
        // Pressed -> Unpressed
        // CompareName(prefabs.Audio_Pressed_Button.name, P_B);
    }
    
    
    // === Method === //
    void P_A() => print("A");
    void P_B() => print("B");
    
    void CompareName(string name, Action callback=null)
    {
        if (currentName.Equals(name))
            callback?.Invoke();
    }

    void Replace(GameObject A, GameObject B)
    {
        A.SetActive(false);
        B.SetActive(true);
    }
}
