using UnityEditor.Rendering;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck instance;

    public Card[] cards;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ShuffleCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            int swapIndex = Random.Range(0, cards.Length - 1);

            Card temp = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = temp;
        }
    }
}
