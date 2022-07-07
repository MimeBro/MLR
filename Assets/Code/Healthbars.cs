using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class Healthbars : MonoBehaviour
{
    public MMProgressBar ProgressBar;
    public Unit target;
    public TextMeshProUGUI hpNumber;

    private void Start()
    {
        target = TeamManager.Instance.GetPlayer();
    }

    private void Update()
    {
        if(target == null) return;
        ProgressBar?.UpdateBar(target.hp, 0 , target.maxhp);
    }

    public void SwitchTarget()
    {
        target = TeamManager.Instance.currentTeam[TeamManager.Instance.memberOnTheField];
    }
}
