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
        posture.OnPostureChanged += PostureUpdate;
        posture.OnPostureReset += ResetPosture;
    }
    
    void OnDisable()
    {
        posture.OnPostureChanged -= PostureUpdate;
        posture.OnPostureReset -= ResetPosture;
    }


    // === Self Method === //
    void PostureUpdate()
    {
        float curr = posture.CurrPosture;
        float max = posture.MaxPosture;
        
        float fill = curr / max;
        SetFills(fill);
    }
    void ResetPosture()
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