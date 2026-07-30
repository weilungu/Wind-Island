using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonEffect : MonoBehaviour
{
    [SerializeField] float scale = 1.1f;

    RectTransform[] rectTransforms;
    RectTransform rect_TF;
    
    bool isSelected = false;
    
    // == Unity Life Cycle == //
    void Awake()
    {
        rectTransforms = GetComponentsInChildren<RectTransform>();
        rect_TF = rectTransforms[1];
    }
    
    // === Self Method === //
    public void SelectEffect()
    {
        if (isSelected) return;
        isSelected = true;
        
        rect_TF.localScale = Vector3.one * scale;
    }
    public void DeselectEffect()
    {
        if (!isSelected) return;
        isSelected = false;
        
        rect_TF.localScale = Vector3.one;
    }
}