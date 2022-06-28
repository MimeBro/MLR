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

    public Transform MonsterSpawnPoint;
    public MoveSlotsManager MoveSlotsManager;
    public bool controlsStopped;
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
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        EnergyCharge();
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
    
    public void StopPlayerControls()
    {
        PlayerController.Instance.CloseBuffer();
        MoveSlotsManager.DisableAllSlots();
    }

    public void StopPlayerControls(float duration)
    {
        PlayerController.Instance.CloseBuffer(duration);
        MoveSlotsManager.DisableAllSlots(duration);
    }

    public void ResumePlayerControls()
    {
        PlayerController.Instance.OpenBuffer();
        MoveSlotsManager.EnableallSlots();
    }
}
