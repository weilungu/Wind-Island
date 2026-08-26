using UnityEngine;
using UnityEngine.EventSystems;

public class MenuInteraction : MonoBehaviour,
    IPointerEnterHandler,
    IPointerClickHandler
{
    MenuNavigation navigation;

    
    // === Unity Life Cycle === //
    void Awake()
    {
        navigation = GetComponent<MenuNavigation>();
    }

    // === Interface API === //
    public void OnPointerEnter(PointerEventData eventData)
    {
        MainMenuButton button =
            eventData.pointerEnter?
                .GetComponentInParent<MainMenuButton>();

        if (button is null) return;

        navigation.Select(button);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenuButton button =
            eventData.pointerClick?
                .GetComponentInParent<MainMenuButton>();

        button?.Invoke();
    }
}
