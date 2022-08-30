using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackDirection{Forward, Backward, Both}
//public enum TypeOfAttack{SimpleProjectile, GuidedProjectile, ArchedProjectile,AreaAttack,DashAttack,Summon}
public class AttackController : MonoBehaviour
{
    [Title("General Attributes")]
    public OldUnit attacker;
    [EnumToggleButtons]
    public AttackDirection attackDirection;
    public int baseDamage;
    
    [HideInInspector]
    public ElementalTypes moveType;

    [Range(1,5)] public int howManyPanelsInFront;
    public bool hitAllPanelsInTheWay;
    public float moveDuration;
    public float startDelay;

    public List<Panel> panels;

    [Space]
    public List<Transform> shootPositions = new List<Transform>();

    public bool stopTimeToAttack;
    public float timeStopDuration;

    public virtual void CastAttack()
    {
        //If there's no attacker, assume the attacker is the player
        panels = UnitTools.GetPanels(attacker != null ? attacker : TeamManager.Instance.GetPlayer(),
            howManyPanelsInFront, attackDirection);
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
