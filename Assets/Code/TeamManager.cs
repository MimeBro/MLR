using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;
    
    [Title("Team Management")]
    public MonsterTeam playersTeam;
    public TeamSlotsManager teamSlotsManager;
    
    [HideInInspector]public MonsterTeam enemyTeam;
    public List<Unit> currentTeam;
    
    [Title("Switch In and Out")] 
    public int memberOnTheField;
    public Transform MonsterSpawnPoint;
    
    private void Start()
    {
        Instance = this;
        SetTeam();
    }

    public void SetTeam()
    {
        if(currentTeam.Any()) return;
        for (var index = 0; index < playersTeam.Monsters.Count; index++)
        {
            var mon = playersTeam.Monsters[index];
            var member = Instantiate(mon, MonsterSpawnPoint.position, Quaternion.identity);
            currentTeam.Add(member);
            member.transform.SetParent(PlayerController.Instance.transform);
            member.gameObject.SetActive(false);
            teamSlotsManager.Team[index].SetButton(member, index);
        }
        
        teamSlotsManager.SetSlots();
        PlayerController.Instance.unit = currentTeam[0];
        memberOnTheField = currentTeam.IndexOf(currentTeam[0]);
        currentTeam[0].gameObject.SetActive(true);
        currentTeam[0].SwitchedIn(LastPanel());
    }

    public Unit GetPlayer()
    {
        return currentTeam[memberOnTheField];
    }

    public Panel GetPlayerPanel()
    {
        return currentTeam[memberOnTheField].currentPanel;
    }

    public MoveSet GetPlayerMoves()
    {
        return currentTeam[memberOnTheField].moveSet;
    }

    public Unit GetEnemy()
    {
        return null;
    }

    public void SwitchMember(int index)
    {
        if(index == memberOnTheField) return;
        
        var lp = LastPanel();
        foreach (var member in currentTeam)
        {
            member.gameObject.SetActive(false);
        }
        memberOnTheField = index;
        PlayerController.Instance.unit = currentTeam[index];
        currentTeam[index].gameObject.SetActive(true);
        currentTeam[index].transform.position = MonsterSpawnPoint.position;
        currentTeam[index].SwitchedIn(lp);
    }

    public Panel LastPanel()
    {
        if (currentTeam[memberOnTheField].currentPanel != null)
        {
            return currentTeam[memberOnTheField].currentPanel;
        }
        else
        {
            return PanelsManager.Instance.PanelList[1];
        }
    }
}
