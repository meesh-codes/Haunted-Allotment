using UnityEditor.Rendering;
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
        cards.Add(card);
    }
}
