using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public MMProgressBar energyBar;
    private Unit target;
    
    private void Start()
    {
    }

    private void Update()
    {
        if (target == null)
        {
            target = TeamManager.Instance.GetPlayer();
            return;
        }
        
        energyText.text = Mathf.FloorToInt(target.energy).ToString();
        energyBar?.UpdateBar(target.energy, 0, target.maxEnergy);
    }
}
