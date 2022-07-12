using System;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour
{
    public KeyCode assignedButton;
    public int teamIndex;
    
    [Title("Setup")]
    public Image monsterImage;
    public Image healthBar;
    public TextMeshProUGUI monsterName;
    
    public bool setup;
    public bool disabled;
    
    public void SetButton(Unit unit, int index)
    {
        teamIndex = index;
        if (unit.stats.nickname)
        {
            monsterName.text = unit.stats.monsterNickname;
        }
        else
        {
            monsterName.text = unit.stats.monsterName;
        }

        monsterImage.sprite = unit.stats.monsterImage;
        setup = true;
    }

    private void Update()
    {
        if (setup && !disabled)
        {
            if (Input.GetKeyDown(assignedButton))
            {
                TeamManager.Instance.SwitchMember(teamIndex);
                TeamManager.Instance.teamSlotsManager.SetSlots();
                TeamManager.Instance.teamSlotsManager.SwitchCooldown();
            }
        }
    }
}
