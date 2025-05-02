using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public float cardSpacing;
    public int maxSize = 8;

    public List<Card> cards;

    public void ArrangeCards()
    {
        if (cards.Count > 0)
        {
            float initialOffset = (cards.Count/2) * cardSpacing;
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

    public void DrawCards(int numberToDraw)
    {
        GameManager gm = FindFirstObjectByType<GameManager>();

        Deck deck = gm.deck;
        deck.ShuffleCards();
        for (int i = 0; i < numberToDraw; i++)
        {
            // draw the first card and remove it from the deck
            if (deck.cards.Count > 0)
            {
                cards.Add(Instantiate(deck.cards[0], new Vector3(0,0,0), Quaternion.identity));
                deck.cards.RemoveAt(0);
            }
        }

        ArrangeCards();
    }

    public void DrawAdditionalCard(int cost)
    {
        GameManager gm = FindFirstObjectByType<GameManager>();

        if (gm.RequestAction(cost))
        {
            DrawCards(1);

            gm.TakeAction(cost);
        }
        else
        {
            // TODO feedback to player
        }
    }

    public void Clear()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }

        cards.Clear();
    }
}
