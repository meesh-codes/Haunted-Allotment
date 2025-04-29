using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int m_DayNumber = 0;
    int m_MaximumDays = 30;

    public int m_Energy = 5;
    public int m_MaxEnergy = 5;

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

    public void PassDay()
    {

        // reset all plants
        Plant[] plants = FindObjectsByType<Plant>(FindObjectsSortMode.None);

        foreach (var plant in plants)
        {
            plant.ResetDay();
        }

        // reset energy
        m_Energy = m_MaxEnergy;
        // todo: save data
        // draw a new hand
        Deck.instance.ShuffleCards();
    }
}
