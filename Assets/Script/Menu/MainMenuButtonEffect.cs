using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonEffect : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] float scale = 1f;

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
    public void OnSelect(BaseEventData eventData)
    {
        Select();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Deselect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
    void Select()
    {
        if (isSelected) return;
        isSelected = true;
        
        rect_TF.localScale = Vector3.one * scale;
    }

    void Deselect()
    {
        if (!isSelected) return;
        isSelected = false;
        
        rect_TF.localScale = Vector3.one;
    }
}