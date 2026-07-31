using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonEffect : MonoBehaviour
{
    [SerializeField] float scale = 1.1f;

    RectTransform[] rect_TF;
    RectTransform target;
    
    bool isSelected = false;
    
    // == Unity Life Cycle == //
    void Awake()
    {
        rect_TF = GetComponentsInChildren<RectTransform>();
        target = rect_TF[1];
    }
    
    // === Self Method === //
    public void SelectEffect()
    {
        if (isSelected) return;
        isSelected = true;
        
        target.localScale = Vector3.one * scale;
    }
    public void DeselectEffect()
    {
        if (!isSelected) return;
        isSelected = false;
        
        target.localScale = Vector3.one;
    }
}