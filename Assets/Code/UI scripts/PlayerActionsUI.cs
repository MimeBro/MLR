using System;
using System.Collections.Generic;
using Code.UI_scripts;
using Code.WeaponScripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionsUI : MonoBehaviour
{
    public Transform attackAction;
    public Transform weaponsContainer;
    public List<WeaponButton> weaponButtons;

    public void Start()
    {
        HideActions();
        HideWeapons();
    }

    public void ShowActions()
    {
        attackAction.gameObject.SetActive(true);
    }

    public void ShowWeapons()
    {
        var playerWpns = BattleManager.Instance.GetPlayer().acquiredWeapons;
        
        weaponsContainer.gameObject.SetActive(true);
        
        foreach (var wpbtn in weaponButtons)
        {
            wpbtn.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < playerWpns.Count; i++)
        {
            weaponButtons[i].assignedWeapon = playerWpns[i];
            weaponButtons[i].assignedWeaponIndex = i;
            weaponButtons[i].gameObject.SetActive(true);
        }
    }

    public void HideWeapons()
    {
        weaponsContainer.gameObject.SetActive(false);
    }

    public void HideActions()
    {
        attackAction.gameObject.SetActive(false);
    }
}
