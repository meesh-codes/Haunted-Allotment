using UnityEngine;

public enum EPlantType
{
    NotAssigned,
    Pea,
    Potato,
    Carrot,
    Lettuce
}

public class Plant : MonoBehaviour
{
    string m_Name;
    bool m_IsWatered = false;
    int m_Health = 3;
    bool m_IsAlive = true;

    int m_GrowthDays = 0;
    bool m_IsHarvestable = false;

    public EPlantType m_PlantType;
    public int m_DaysToSeedling = 1;
    public int m_DaysToMid = 3;
    public int m_DaysToFull = 7;
    public Sprite[] m_GrowthSprites;
    public Sprite m_DrySoilSprite;
    public Sprite m_WetSoilSprite;
    public GameObject m_SoilObject;
    public GameObject m_PlantObject;

    public void ResetDay()
    {
        if (m_IsWatered == false && m_IsHarvestable == false)
        {
            m_Health--;

            if (m_Health == 0)
            {
                m_IsAlive = false;
            }
        }
        else
        {
            // is watered, progress growth
            m_GrowthDays++;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (m_DaysToSeedling <= m_GrowthDays && m_GrowthDays <= m_DaysToMid) // seedling growth
            {
                sr.sprite = m_GrowthSprites[1];
            }
            if (m_DaysToMid <= m_GrowthDays && m_GrowthDays <= m_DaysToFull) // mid-level growth
            {
                sr.sprite = m_GrowthSprites[2];
            }
            if (m_DaysToFull <= m_GrowthDays) // full growth
            {
                sr.sprite = m_GrowthSprites[3];
                m_IsHarvestable = true;
            }

            m_IsWatered = false;
        }
    }

    public void WaterPlant()
    {
        m_IsWatered = true;
    }

}
