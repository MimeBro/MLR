using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;
    
    [Title("Team Management")]
    public MonsterTeam playersTeam;

    [HideInInspector]public MonsterTeam enemyTeam;
    public List<Unit> currentTeam;
    
    [Title("Switch In and Out")] 
    public int memberOnTheField;
    public Transform MonsterSpawnPoint;
    public Transform playerController;
    
    private void Start()
    {
        Instance = this;
        SetTeam();
    }

    public void SetTeam()
    {
        if(currentTeam.Any()) return;
        foreach (var mon in playersTeam.Monsters)
        {
            var member = Instantiate(mon, MonsterSpawnPoint.position, Quaternion.identity);
            currentTeam.Add(member);
            member.transform.SetParent(playerController);
            member.gameObject.SetActive(false);
            member.SwitchedIn(LastPanel());
        }
        
        currentTeam[0].gameObject.SetActive(true);
        memberOnTheField = currentTeam.IndexOf(currentTeam[0]);
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
        foreach (var member in currentTeam)
        {
            member.gameObject.SetActive(false);
        }
        memberOnTheField = index;
        currentTeam[index].gameObject.SetActive(true);
        currentTeam[index].transform.position = MonsterSpawnPoint.position;
        currentTeam[index].SwitchedIn(LastPanel());
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
