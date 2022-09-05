using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.CommonScripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackDirection{Forward, Backward, Both}
public class AttackController : MonoBehaviour
{
    [Title("General Attributes")]
    public Unit attacker;
    [EnumToggleButtons]
    public AttackDirection attackDirection;
    public int baseDamage;
    
    public float moveDuration;
    public float startDelay;
    
    [Space]
    public List<Transform> shootPositions = new List<Transform>();

    public bool stopTimeToAttack;
    public float timeStopDuration;

    public virtual async Task CastAttack(Unit attacker)
    {
        this.attacker = attacker;
    }
    
    public IEnumerator StopTime()
    {
        DOVirtual.Float(1, 0, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
        yield return new WaitForSecondsRealtime(timeStopDuration);
        DOVirtual.Float(0, 1, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
    }
}
