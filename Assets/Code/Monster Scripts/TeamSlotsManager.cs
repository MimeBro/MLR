using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TeamSlotsManager : MonoBehaviour
{
    public TeamSlot[] Team;
    [SerializeField] private List<TeamSlot> activeMembers = new List<TeamSlot>(3);

    private void Awake()
    {
        Team = GetComponentsInChildren<TeamSlot>();
    }

    public void SetSlots()
    {
        activeMembers.Clear();
        for (int i = 0; i < Team.Length; i++)
        {
            if (Team[i].teamIndex != TeamManager.Instance.memberOnTheField && Team[i].setup)
            {
                Team[i].gameObject.SetActive(true);
                activeMembers.Add(Team[i]);
            }
            else
            {
                Team[i].gameObject.SetActive(false);
            }
        }
        AssignButtons();
    }

    private void AssignButtons()
    {
        if(activeMembers.Count >= 1) activeMembers[0].assignedButton = KeyCode.Alpha1;
        if(activeMembers.Count >= 2) activeMembers[1].assignedButton = KeyCode.Alpha2;
        if(activeMembers.Count >= 3) activeMembers[2].assignedButton = KeyCode.Alpha3;
    }

    public async void SwitchCooldown()
    {
        foreach (var member in Team)
        {
            member.disabled = true;
        }

        var cooldown = Time.time + 1f;
        while (Time.time < cooldown)
        {
            await Task.Yield();
        }
        
        foreach (var member in Team)
        {
            member.disabled = false;
        }
    }
}
