using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackDirection{Forward, Backward, Both}
public enum TypeOfAttack{SimpleProjectile, GuidedProjectile, ArchedProjectile,AreaAttack,DashAttack,Summon}
public class AttackController : MonoBehaviour
{
    [Title("General Attributes")]
    [EnumToggleButtons]
    public AttackDirection attackDirection;
    public Unit attacker;
    [HideInInspector]
    public ElementalTypes moveType;
    
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
    public float startDelay;

    [Title("Guided Projectiles")]
    public GuidedProjectile guidedProjectile;
    public float guidedProjectileSpeed;
    public int guidedProjectileDamage;
    public float gapBetweenSpawn;
    public bool usePlayerShootPoint;

    [Title("Area Attack")]  
    public AreaAttack areaAttack;
    public int damagePerHit;
    public float hitsPerSecond;
    public float areaDuration;
    
    [Title("Summon")]
    public MonsterAttack monsterToSummon;
    public float summonAttackDuration;

    [Title("Dash Attack")] 
    public DashAttack dashAttack;
    public bool backAndForth;
    public float dashSpeed;
    public float dashDuration;

    public async void CastAttack()
    {
        //If there's no attacker, assume the attacker is the player
        panels = UnitTools.GetPanels(attacker != null ? attacker : TeamManager.Instance.GetPlayer(),
            howManyPanelsInFront, attackDirection);

        var end = Time.time + startDelay;
        
        while (Time.time < end)
        {
            await Task.Yield();
        }

        switch (TypeOfAttack)
        {
            case TypeOfAttack.GuidedProjectile:
                await GuidedProjectile();
                break;
            case TypeOfAttack.AreaAttack:
                await AreaAttack();
                break;
            case TypeOfAttack.Summon:
                Summon();
                break;
            case TypeOfAttack.DashAttack:
                await DashAttack();
                break;
            default:
                return;
        }
        gameObject.SetActive(false);
    }

    private Task DashAttack()
    {
        var dash = Instantiate(dashAttack);
        dash.attacker = attacker;
        dash.targetPanel = panels[panels.Count - 1];
        dash.backAndForth = backAndForth;
        dash.direction = attackDirection;
        dash.dashSpeed = dashSpeed;
        dash.dashDuration = dashDuration;
        dash.side = attacker.side;
        dash.StartDash();
        return Task.CompletedTask;
    }

    private void Summon()
    {
        var mon = Instantiate(monsterToSummon);
        mon.StartAttack();
        Destroy(gameObject);
    }
    

    private Task AreaAttack()
    {
        var aatk = Instantiate(areaAttack, shootPositions[0].position, Quaternion.identity);
        aatk.damagePerHit = damagePerHit;
        aatk.secondsPerHit = hitsPerSecond;
        aatk.attacker = attacker;
        aatk.side = attacker.side;
        Destroy(aatk.gameObject,areaDuration);
        return Task.CompletedTask;
    }

    private async Task GuidedProjectile()
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
        
        if (attackDirection == AttackDirection.Both)
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
             gp = Instantiate(guidedProjectile, TeamManager.Instance.GetPlayer().shootPoint.position, Quaternion.identity);
        }
        else 
        {
             gp = Instantiate(guidedProjectile, shootPositions[ShootPositionIndex].position, Quaternion.identity);
        }
        gp.target = panels[panelIndex].transform;
        gp.speed = guidedProjectileSpeed;
        gp.baseDamage = guidedProjectileDamage;
        gp.attacker = attacker;
        gp.side = attacker.side;
        gp.element = moveType;

        while (Time.time < end) await Task.Yield();
    }

    public void ArchedProjectile()
    {
        
    }
    
    public IEnumerator StopTime()
    {
        DOVirtual.Float(1, 0, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
        yield return new WaitForSecondsRealtime(timeStopDuration);
        DOVirtual.Float(0, 1, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
    }
}
