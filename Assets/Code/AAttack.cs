using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackDirection{Forward, Backward, Both}
public enum TypeOfAttack{SimpleProjectile, GuidedProjectile, ArchedProjectile,AreaAttack,Passive,Summon}
public class AAttack : MonoBehaviour
{
    [Title("General Attributes")]
    [EnumToggleButtons]
    public AttackDirection AttackDirection;
    
    
    [EnumToggleButtons]
    public Sides AttackersSide;
    
    [EnumToggleButtons]
    public TypeOfAttack TypeOfAttack;
    
    [Range(1,5)] public int howManyPanelsInFront;
    public bool hitAllPanelsInTheWay;

    public List<Panel> panels;

    [Space]
    public List<Transform> shootPositions = new List<Transform>();

    public bool stopTimeToAttack;
    public float timeStopDuration;
    public float moveDuration;

    [Title("Guided Projectiles")]
    public GuidedProjectile guidedProjectile;

    public float guidedProjectileSpeed;
    public int guidedProjectileDamage;
    public float startDelay;
    public float gapBetweenSpawn;
    public bool usePlayerShootPoint;

    [Title("Area Attack")]  
    public AreaAttack areaAttack;
    public int damagePerHit;
    public float hitsPerSecond;
    public float areaDuration;

    [Title("Passive")] 
    public PassiveMove passiveMove;
    public MoveButton callerButton;

    [Title("Summon")]
    public MonsterAttack monsterToSummon;
    public float summonAttackDuration;

    

    public async void CastAttack()
    {
        var end = Time.time + startDelay;
        GetPanels();
        while (Time.time < end)
        {
            await Task.Yield();
        }
        
        switch (TypeOfAttack)
        {
          case  TypeOfAttack.GuidedProjectile:
              GuidedProjectile();
              break;
          case TypeOfAttack.AreaAttack:
              AreaAttack();
              break;
          case TypeOfAttack.Summon:
              Summon();
              break;
          case TypeOfAttack.Passive:
              PassiveMove();
              break;
        }
    }

    private void Summon()
    {
        var mon = Instantiate(monsterToSummon);
        mon.StartAttack();
        Destroy(gameObject);
    }

    private void PassiveMove()
    {
        var pas = Instantiate(passiveMove);
        pas.callerButton = callerButton;
        Destroy(gameObject);
    }

    private void AreaAttack()
    {
        var aatk = Instantiate(areaAttack, shootPositions[0].position, Quaternion.identity);
        aatk.damagePerHit = damagePerHit;
        aatk.secondsPerHit = hitsPerSecond;
        aatk.side = AttackersSide; 
        Destroy(aatk.gameObject,areaDuration);
    }

    private async void GuidedProjectile()
    {
        
        if (hitAllPanelsInTheWay)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                await InstantiateGuidedProjectile(i, 0, gapBetweenSpawn);
            }
            return;
        }

        await InstantiateGuidedProjectile(panels.Count - 1, 0,gapBetweenSpawn);
        
        if (AttackDirection == AttackDirection.Both)
        {
            await InstantiateGuidedProjectile(0, 0, 0);
        }
    }

    private async Task InstantiateGuidedProjectile(int panelIndex,int ShootPositionIndex,float duration)
    {
        var end = Time.time + duration;
        GuidedProjectile gp;

        if (usePlayerShootPoint)
        {
             gp = Instantiate(guidedProjectile, PlayerController.Instance.shootPoint.position, Quaternion.identity);
        }
        else
        {
             gp = Instantiate(guidedProjectile, shootPositions[ShootPositionIndex].position, Quaternion.identity);
        }
        gp.target = panels[panelIndex].transform;
        gp.speed = guidedProjectileSpeed;
        gp.damage = guidedProjectileDamage;
        gp.side = AttackersSide;
        
        while (Time.time < end)
        {
            await Task.Yield();
        }
    }

    public void ArchedProjectile()
    {
        
    }

    private void GetPanels()
    {
        panels.Clear();
        var playerPanelIndex = GameManager.Instance.PanelList.IndexOf(PlayerController.Instance.unit.currentPanel);
        var playerpanelF = playerPanelIndex + 1;
        var playerpanelB = playerPanelIndex - 1;
        var lastPanel = GameManager.Instance.PanelList.Count;
        
        switch (AttackDirection)
        {
            case AttackDirection.Forward:
                if (playerPanelIndex + howManyPanelsInFront < lastPanel)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (GameManager.Instance.PanelList.Count - playerpanelF); i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                break;
            
            case AttackDirection.Backward:
                if (playerPanelIndex - howManyPanelsInFront >= 0)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  playerPanelIndex; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                break;
            case AttackDirection.Both:
                if (playerPanelIndex - howManyPanelsInFront >= 0)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Insert(0,GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  playerPanelIndex; i++)
                    {
                        panels.Insert(0,GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                
                if (playerPanelIndex + howManyPanelsInFront < lastPanel)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (GameManager.Instance.PanelList.Count - playerpanelF); i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                break;
        }
    }
    
    public IEnumerator StopTime()
    {
        DOVirtual.Float(1, 0, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
        yield return new WaitForSecondsRealtime(timeStopDuration);
        DOVirtual.Float(0, 1, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
    }
}
