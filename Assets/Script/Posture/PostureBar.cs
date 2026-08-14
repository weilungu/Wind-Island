using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image[] fills = new Image[2];
    
    [Header("Target")]
    [SerializeField] PlayerPosture posture;

    
    // === Unity Life Cycle === //
    void OnEnable()
    {
        posture.OnChanged += Update;
        posture.OnReset += Reset;
    }
    
    void OnDisable()
    {
        posture.OnChanged -= Update;
        posture.OnReset -= Reset;
    }


    // === Self Method === //
    void Update()
    {
        float curr = posture.CurrPosture;
        float max = posture.MaxPosture;
        
        float fill = curr / max;
        SetFills(fill);
    }
    void Reset()
    {
        const float fill = 0f;

        SetFills(fill);
    }
    
    void SetFills(float fill)
    {
        foreach (Image f in fills)
            f.fillAmount = fill;
    }
}