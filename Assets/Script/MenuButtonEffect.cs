using UnityEngine;
using UnityEngine.EventSystems;

public class TMPro_Selected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] float scale = 1f;

    RectTransform[] rectTransforms;
    RectTransform rect_TF;

    Vector3 originalPosition;
        
    void Awake()
    {
        rectTransforms = GetComponentsInChildren<RectTransform>();
        rect_TF = rectTransforms[1];
    }

    public void OnSelect(BaseEventData eventData)
    {
        rect_TF.localScale = Vector3.one * scale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        rect_TF.localScale = Vector3.one;
    }
}