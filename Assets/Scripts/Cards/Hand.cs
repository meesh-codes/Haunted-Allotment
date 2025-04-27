using UnityEngine;

public class Hand : MonoBehaviour
{
    public float cardSpacing;
    public int maxSize = 8;

    public Card[] cards;

    public void ArrangeCards()
    {
        if (cards.Length > 0)
        {
            float initialOffset = (cards.Length/2) * cardSpacing;
            float initialXPos = transform.position.x - initialOffset;
            Vector3 spawnPos = new Vector3(initialXPos, transform.position.y, 0);

            int layer = 0;

            foreach (Card card in cards)
            {
                card.m_HandPosition = spawnPos;

                card.transform.position = spawnPos;
                spawnPos.x = spawnPos.x + cardSpacing;

                SpriteRenderer sr = card.GetComponent<SpriteRenderer>();
                sr.sortingOrder = layer;
                layer++;
            }
        }
        

    }
}
