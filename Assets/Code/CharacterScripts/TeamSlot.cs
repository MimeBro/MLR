using System;
using System.Linq;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour
{
    public KeyCode assignedButton;
    public int teamIndex;
    
    [Title("Setup")]
    public Image monsterImage;
    public MMProgressBar healthBar;
    public TextMeshProUGUI monsterName;

    [FormerlySerializedAs("setUnit")] public OldUnit setOldUnit;
    public bool setup;
    public bool disabled;
    
    public void SetButton(OldUnit oldUnit, int index)
    {
        teamIndex = index;
        setOldUnit = oldUnit;
        
        if (oldUnit.stats.nickname)
        {
            monsterName.text = oldUnit.stats.monsterNickname;
        }
        else
        {
            monsterName.text = oldUnit.stats.monsterName;
        }

        monsterImage.sprite = oldUnit.stats.monsterPortrait;
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

        if (setOldUnit != null) healthBar?.UpdateBar(setOldUnit.hp, 0, setOldUnit.maxhp);
    }
}
