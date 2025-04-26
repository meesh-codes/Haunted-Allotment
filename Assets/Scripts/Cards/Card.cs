using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Card : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler,
    IDragHandler
{
    private bool m_IsHovered = false;
    private bool m_IsSelected = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsHovered = true;
        Debug.Log("Hover Start");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsHovered = false;
        Debug.Log("Hover End");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_IsHovered)
        {
            m_IsSelected = true;
            Debug.Log("Selected");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_IsSelected)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = mousePos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_IsSelected)
        {
            Debug.Log("Released");
            m_IsSelected = false;
        }
    }

    void PlayCard()
    {
        Debug.Log("Playing Card");
    }
}
