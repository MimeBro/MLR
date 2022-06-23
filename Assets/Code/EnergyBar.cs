using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public MMProgressBar energyBar;


    private void Update()
    {
        energyText.text = Mathf.FloorToInt(GameManager.Instance.energy).ToString();
        energyBar?.UpdateBar(GameManager.Instance.energy, 0, GameManager.Instance.maxEnergy);
    }
}
