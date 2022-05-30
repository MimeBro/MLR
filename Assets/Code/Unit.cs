using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Sides side;
    public int hp;
    public int maxhp;
    public Panel currentPanel;
    public UnitStatus unitStatus;
    public ManaBarSO manaBar;
    
    private void Start()
    {
        maxhp = 100;
        hp = maxhp;
    }

    public void TakeDamage(int d)
    {
        hp -= d;
    }

    public void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxhp);
        
        if (hp <= 0)
        {
            Die();
        }

        WetDebuff();
    }

    public void BurnPlayerTest()
    {
        InflictStatus(OnHitEffects.HEAT, 5);
    }

    public void ColdPlayerTest()
    {
        InflictStatus(OnHitEffects.COLD, 5);
    }

    public void WetPlayerTest()
    {
        InflictStatus(OnHitEffects.WET, 5);
    }

    public void PoisonedPlayerTest()
    {
        InflictStatus(OnHitEffects.POISONED, 5);
    }

    public void GroundedPlayerTest()
    {
        InflictStatus(OnHitEffects.GROUNDED, 10);
    }


    public void InflictStatus(OnHitEffects effects, float duration)
    {
        switch (effects)
        {
            case OnHitEffects.HEAT:
                Heat(duration);
                break;

            case OnHitEffects.WET:
                Wet(duration);
                break;

            case OnHitEffects.COLD:
                Cold(duration);
                break;

            case OnHitEffects.POISONED:
                Poisoned(duration);
                break;

            case OnHitEffects.GROUNDED:
                Grounded(duration);
                break;

            case OnHitEffects.STUNNED:
                break;

            case OnHitEffects.SHOCKED:
                break;
        }
    }

    /// Deals a fixed amount of damage overtime.
    private void Heat(float duration)
    {
        if (unitStatus == UnitStatus.HEAT) return;
        IEnumerator burningCorroutine = HeatSequence(duration);
       StartCoroutine(burningCorroutine);
    }

    private IEnumerator HeatSequence(float duration)
    {
        unitStatus = UnitStatus.HEAT;
        var newDuration = duration;
    
        while (newDuration > 0)
        {
            TakeDamage(1);
            Debug.Log("Burning for " + newDuration + " more seconds");
            yield return new WaitForSeconds(0.5f);
            newDuration -= 0.5f;
        }

        //Place Holder
        unitStatus = UnitStatus.NORMAL;
    }

    //Slows down the ManaBar regeneration rate
    private void Cold(float duration)
    {
        if (unitStatus == UnitStatus.COLD) return;
        StartCoroutine(ColdSequence(duration));
    }

    private IEnumerator ColdSequence(float duration)
    {
        unitStatus = UnitStatus.COLD;
        var newMPS = manaBar.manaPerSecond * 0.7f;
        manaBar.manaPerSecond -= newMPS;
        Debug.Log("Cold for " + duration + " seconds, MPS = " + manaBar.manaPerSecond);
        yield return new WaitForSeconds(duration);
        unitStatus = UnitStatus.NORMAL;
        manaBar.manaPerSecond += newMPS;
        Debug.Log("Cold Ended, MPS = " + manaBar.manaPerSecond);
    }

    //Spreads water to every tile 
    private void Wet(float duration)
    {
        if (unitStatus == UnitStatus.WET) return;
        StartCoroutine(WetSequence(duration));
    }

    private IEnumerator WetSequence(float duration)
    {
        unitStatus = UnitStatus.WET;
        Debug.Log("Wet for " + duration + " seconds");
        yield return new WaitForSeconds(duration);
        Debug.Log("No longer wet");
        unitStatus = UnitStatus.NORMAL;
    }

    private void WetDebuff()
    {
        if (unitStatus != UnitStatus.WET) return;
        if (currentPanel == null) return;
        currentPanel.ChangeStatus(PanelStatus.WET);
    }

    //Deal 1% of the unit's max HP every second.
    private void Poisoned(float duration)
    {
        if (unitStatus == UnitStatus.POISONED) return;
        StartCoroutine(PoisonedSequence(duration));
    }

    private IEnumerator PoisonedSequence(float duration)
    {
        unitStatus = UnitStatus.POISONED;
        var newDuration = duration;
        var TenPercentMaxHP = maxhp * 0.1f;
        var OnePercentMaxHP = TenPercentMaxHP * 0.1f;

        while (newDuration > 0)
        {
            TakeDamage((int)OnePercentMaxHP);
            yield return new WaitForSeconds(1);
            newDuration --;
        }
        unitStatus = UnitStatus.NORMAL;
    }

    private void Grounded(float duration)
    {
        if (unitStatus == UnitStatus.GROUNDED) return;
        StartCoroutine(GroundedSequence(duration));
    }

    private IEnumerator GroundedSequence(float duration)
    {
        unitStatus = UnitStatus.GROUNDED;
        yield return new WaitForSeconds(duration);
        unitStatus = UnitStatus.NORMAL;
    }

    private void Die()
    {
        Debug.Log("Died");
        //currentPanel = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Panel>())
        {
            currentPanel = other.GetComponent<Panel>();
        }
    } 
}