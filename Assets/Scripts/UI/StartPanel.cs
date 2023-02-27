using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartPanel : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnPanelTouched;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameStateManager.Instance.SetState(GameState.Game);
        OnPanelTouched?.Invoke();
    }

}
