using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public List<Card> cards;

    public void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int swapIndex = Random.Range(0, cards.Count - 1);

            Card temp = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = temp;
        }
    }

    public void AddCard(Card card)
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm.RequestAction(card.m_GatherCost))
        {
            cards.Add(card);
            gm.TakeAction(card.m_GatherCost);
        }
        else
        {
            // TODO feedback to player
        }
    }

    public void AddRandomCard(int energyCost)
    {
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm.RequestAction(energyCost))
        {
            int cardIndex = Random.Range(0, gm.cardTypes.Length);
            cards.Add(gm.cardTypes[cardIndex]);

            gm.TakeAction(energyCost);
        }
        else
        {
            // TODO feedback to player
        }
    }

    public void ReturnCardToDeck(Card card)
    {
        cards.Add(card);
    }
}
