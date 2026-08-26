using UnityEngine;

public class MainMenuButtonEffect : MonoBehaviour
{
    [SerializeField] private float scale = 1.1f;

    private RectTransform[] rectTransforms;
    private RectTransform target;

    // === Unity Life Cycle === //
    private void Awake()
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
