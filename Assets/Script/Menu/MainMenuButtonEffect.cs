using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonEffect : MonoBehaviour
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
    
    
    // === Self Method === //
    public void SelectEffect()
    {
        target.localScale = Vector3.one * scale;
    }
    public void DeselectEffect()
    {
        target.localScale = Vector3.one;
    }
}