using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public MMProgressBar energyBar;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void Update()
    {
        energyText.text = Mathf.FloorToInt(gm.energy).ToString();
        energyBar?.UpdateBar(gm.energy, 0, gm.maxEnergy);
    }
}
