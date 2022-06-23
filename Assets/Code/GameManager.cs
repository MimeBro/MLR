using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private bool _blinking = false;
    public List<Panel> PanelList = new List<Panel>();
    public MoveSet playerMoveSet;
    public static GameManager Instance;
    
    public float energy;
    public float maxEnergy = 5;
    public float energyRegenerationRate;
    
    private void Start()
    {
        Instance = this;
        energy = maxEnergy;
    }

    public void AddMoveToSet(MovesSO movesSo)
    {
        playerMoveSet.AddMove(movesSo);
    }
    
    //The Panel where the player is currently standing on
    public Panel PlayerPanel()
    {
        foreach (var panel in PanelList)
        {
            if (panel.occupier != null)
            {
                if (panel.occupier.transform.GetComponent<PlayerController>())
                {
                    return panel;
                }
            }
        }
        
        return null;
    }

    private void Update()
    {
        EnergyCharge();
        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }

    public void UseEnergy(float cost)
    {
        energy -= cost;
    }

    public void EnergyCharge()
    {
        if (energy < maxEnergy)
        {
            energy += energyRegenerationRate * Time.deltaTime;
        }
    }
}
