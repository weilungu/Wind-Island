using UnityEngine;

public class MainMenuButtonEffect : MonoBehaviour
{
    [SerializeField] float scale = 1.1f;

    RectTransform[] rectTransforms;
    RectTransform target;

    // === Unity Life Cycle === //
    void Awake()
    {
        rectTransforms = GetComponentsInChildren<RectTransform>();
        target = rectTransforms.Length > 1 ? rectTransforms[1] : GetComponent<RectTransform>();
    }

    // === Self API === //
    public void SelectEffect()
    {
        target.localScale = Vector3.one * scale;
    }

    public void DeselectEffect()
    {
        target.localScale = Vector3.one;
    }
}
