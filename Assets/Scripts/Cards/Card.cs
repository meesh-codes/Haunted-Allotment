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
    private bool m_IsPlayable = false;

    public Vector3 m_HandPosition;
    private float m_HoverOffset = 0.4f;

    public void SetIsPlayable(bool toSet)
    {
        m_IsPlayable = toSet;
    }

    public virtual void PlayCard()
    {
        Debug.Log("Playing Card");
        //Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsHovered = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + m_HoverOffset, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsHovered = false;
        transform.position = m_HandPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_IsHovered)
        {
            m_IsSelected = true;
        }
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Vector3 screenPosition = eventData.position;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        worldPosition.z = 0f;
        return worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_IsSelected)
        {
            Vector3 mousePos = GetMouseWorldPosition(eventData);
            transform.position = mousePos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_IsSelected && m_IsPlayable)
        {
            PlayCard();
        }
        else
        {
            transform.position = m_HandPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayableArea"))
        {
            Debug.Log("Card is playable");
            SetIsPlayable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayableArea"))
        {
            Debug.Log("Card is not playable");
            SetIsPlayable(false);
        }
    }
}
