using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    int m_DayNumber = 1;
    int m_MaximumDays = 30;

    public int m_Energy = 5;
    public int m_MaxEnergy = 5;

    public Deck deck;
    public Hand hand;
    public int m_StartingHandSize = 3;

    public Card[] cardTypes;

    private bool m_IsPaused = false;
    private bool m_CanPause = false;
    private GameObject m_ActiveUIOverlay;

    private void Awake()
    {
        // TODO load values from save data

        m_IsPaused = false;
        m_CanPause = true;
    }

    public void Start()
    {
        StartForagePhase();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && m_CanPause)
        {
            m_CanPause = false;
            SetPause(!m_IsPaused);
        }

        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            m_CanPause = true;
        }
    }

    public void UpdateEnergyText()
    {
        GameObject[] energyTexts = GameObject.FindGameObjectsWithTag("EnergyText");

        foreach (var text in energyTexts)
        {
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();

            textComponent.text = "Energy: " + m_Energy.ToString();
        }
    }

    public void UpdateDayText()
    {
        GameObject[] dayTexts = GameObject.FindGameObjectsWithTag("DayText");

        foreach (var text in dayTexts)
        {
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();

            textComponent.text = "Day " + m_DayNumber.ToString();
        }
    }

    public void StartForagePhase()
    {
        GameObject forageUIObj = GameObject.FindGameObjectWithTag("ForagingScreen");
        GameObject gardeningUIObj = GameObject.FindGameObjectWithTag("GardeningScreen");

        ButtonManager bm = GameObject.FindFirstObjectByType<ButtonManager>();
        bm.EnableObject(forageUIObj);
        bm.DisableObject(gardeningUIObj);

        m_ActiveUIOverlay = forageUIObj;

        UpdateEnergyText();
        UpdateDayText();
    }

    public void StartGardeningPhase()
    {
        GameObject forageUIObj = GameObject.FindGameObjectWithTag("ForagingScreen");
        GameObject gardeningUIObj = GameObject.FindGameObjectWithTag("GardeningScreen");

        ButtonManager bm = GameObject.FindFirstObjectByType<ButtonManager>();
        bm.EnableObject(gardeningUIObj);
        bm.DisableObject(forageUIObj);

        m_ActiveUIOverlay = gardeningUIObj;

        UpdateEnergyText();

        hand.DrawCards(m_StartingHandSize);
    }

    public void SetPause(bool toSet)
    {
        m_IsPaused = toSet;
        GameObject pausePanel = GameObject.FindGameObjectWithTag("PauseMenu");
        pausePanel.GetComponent<CanvasGroup>().interactable = m_IsPaused;

        ButtonManager bm = GameObject.FindFirstObjectByType<ButtonManager>();

        bm.ToggleObject(pausePanel);
        bm.ToggleObject(m_ActiveUIOverlay);
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
        deck.ShuffleCards();

        hand.Clear();
        StartForagePhase();
    }

    public bool RequestAction(int energyCost)
    {
        return m_Energy >= energyCost;
    }

    public void TakeAction(int energyCost)
    {
        m_Energy -= energyCost;
        UpdateEnergyText();
    }
}
