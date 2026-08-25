using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu_ButtonEffect : ButtonEffect
{
    [SerializeField] float scale = 1.1f;

    RectTransform[] rect_TF;
    RectTransform target;
    
    bool isSelected;
    
    
    // == Unity Life Cycle == //
    void Awake()
    {
        rect_TF = GetComponentsInChildren<RectTransform>();
        target = rect_TF[1];
    }
    
    
    // === API === //
    public override void SelectEffect()
    {
        target.localScale = Vector3.one * scale;
    }
    
    public override void DeselectEffect()
    {
        target.localScale = Vector3.one;
    }
}