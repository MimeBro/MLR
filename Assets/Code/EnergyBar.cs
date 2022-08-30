using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public MMProgressBar energyBar;
    private OldUnit target;

    public void SetTarget()
    {
        target = TeamManager.Instance.GetPlayer();
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        
        energyText.text = Mathf.FloorToInt(target.energy).ToString();
        energyBar?.UpdateBar(target.energy, 0, target.maxEnergy);
    }
    
    
    
    
}
