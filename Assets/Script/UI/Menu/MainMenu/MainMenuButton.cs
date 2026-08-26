using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    MainMenuButtonEffect effect;
    Button button;

    // === Unity Life Cycle === //
    void Awake()
    {
        effect = GetComponent<MainMenuButtonEffect>();
        button = GetComponent<Button>();
    }

    // === Self API === //
    public void Select()
    {
        effect.SelectEffect();
    }

    public void Deselect()
    {
        effect.DeselectEffect();
    }

    public void Invoke()
    {
        button.onClick.Invoke();
    }
}
