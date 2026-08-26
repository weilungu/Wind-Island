using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private MainMenuButtonEffect effect;
    private Button button;

    // === Unity Life Cycle === //
    private void Awake()
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
