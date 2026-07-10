using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerInputHandler inputHandler;
    public void OnPointerDown(PointerEventData eventData)
    {
        inputHandler.OnFireDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        inputHandler.OnFireUp();
    }
}

