using System;
using DG.Tweening;
using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using Sirenix.OdinInspector;
using UnityEngine;

//Players face right, Enemies Face Left
public enum FacingDirection{RIGHT, LEFT}
public enum UnitState{STANDING, DODGING}

public class Unit : MonoBehaviour
{
    [Title("Stats")] public MonsterSO stats;
    [EnumToggleButtons] public Sides side;
    public int level;

    [ProgressBar(0, "maxhp", 224, 0, 0, Height = 30)]
    public int hp;

    [HideInInspector] public int maxhp;

    [ProgressBar(0, "maxEnergy", 240, 176, 0, Segmented = true)]
    public float energy;

    [HideInInspector] public int maxEnergy;

    [ProgressBar(0, "expToNextLevel")] public int currentExp;
    public int expToNextLevel;
    
    [Title("Moves")] 
    public MoveSet moveSet;

    [Title("Other")] 
    public Panel currentPanel;
    public Panel lastPanel;
    
    public UnitState uState;
    public Vector2 boxSize;
    
    [Title("Setup")] public float yposition;
    public Transform shootPoint;
    public GameEvent diedEvent;
    public GameEvent monsterEntered;

    public MMFeedbacks DamageFeedback;
    public GameEvent takeDamageEvent;

    [Title("Test")] 
    public bool refillHP;
    public bool infiniteEnergy;

    private UnitMovement unitMovement;
    
    private void Start()
    {
        SetStats();
        unitMovement = GetComponent<UnitMovement>();
    }

    public void SetStats()
    {
        hp = stats.currentHp;
        maxhp = stats.maxHp;
        energy = stats.maxEnergy;
    }

    public void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxhp);
        hp = stats.currentHp;
        level = stats.level;
        currentExp = stats.currentExp;
        expToNextLevel = stats.expToNextLevel[stats.level - 1];

        Die();
        CheckPanel();
        EnergyCharge();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExp(1000);
        }
    }

    public void UseEnergy(int amount)
    {
        if (infiniteEnergy) return;
        energy -= amount;
    }

    public void EnergyCharge()
    {
        if (side != Sides.PLAYER) return;
        energy = Mathf.Clamp(energy, 0, maxEnergy);

        if (energy < maxEnergy)
        {
            energy += stats.energyRegenerationRate * Time.deltaTime;
        }
    }

    #region Damage Calculations

    //Deals damage to the unit without any modifiers applied
    public void TakeDamage(int damage)
    {
        DamageFeedback?.PlayFeedbacks(transform.position, damage);

        stats.currentHp -= damage;
        takeDamageEvent?.Raise();
    }

    //Deals damage to the unit based on stats, contact type and element
    public void TakeDamage(int damage, ContactType contactType, ElementalTypes elementalTypes)
    {
        var finalDamage = CalculateDamage(damage, contactType, elementalTypes);
        
        DamageFeedback?.PlayFeedbacks(transform.position, finalDamage);
        if (finalDamage < 1) finalDamage = 1;
        stats.currentHp -= finalDamage;
        takeDamageEvent?.Raise();
    }
    
    public int CalculateDamage(int damage, ContactType contactType, ElementalTypes elementalTypes)
    {
        return contactType switch
        {
            ContactType.Physical => Mathf.FloorToInt(
                damage * ElementalInteractions.ElementalInteraction(elementalTypes, stats.primaryType) - stats.defense),
            ContactType.Special => Mathf.FloorToInt(
                damage * ElementalInteractions.ElementalInteraction(elementalTypes, stats.primaryType) -
                stats.specialDefense),
            _ => Mathf.FloorToInt(
                damage * ElementalInteractions.ElementalInteraction(elementalTypes, stats.primaryType))
        };
    }
    
    //Makes the unit receive on hit effects
    public void CastHitEffect(HitEffects onHitEffect)
    {
        switch (onHitEffect)
        {
            case HitEffects.KNOCKUP:
                OnHitEffects.KnockUp(this);
                break;
            case HitEffects.KNOCKBACK:
                OnHitEffects.Knockback(this,1);
                break;
            case HitEffects.PUSHBACK:
                OnHitEffects.Pushback(this);
                break;
            default:
                return;
        }
    }
    
    #endregion
    public void Heal(int healAmount)
    {
        hp += healAmount;
    }

    public void GainExp(int expAmount)
    {
        stats.AddExp(expAmount);
    }

    public void CheckPanel()
    {
        var box = Physics2D.OverlapBox(transform.position, boxSize, 0, LayerMask.GetMask("Panels"));
        currentPanel = box?.GetComponent<Panel>();
        
        if (currentPanel != null)
        {
            lastPanel = currentPanel;
        }
    }

    public void SwitchedIn(Panel panel)
    {
        var panelPos = panel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);
        transform.DOJump(destination, 5, 1, 1).SetEase(Ease.Linear);
        monsterEntered?.Raise();
    }

    public void SwitchedOut()
    {
        var exitPos = TeamManager.Instance.MonsterSpawnPoint.position;
        transform.DOJump(exitPos, 5, 1, 1).SetEase(Ease.Linear);
    }

    private void Die()
    {
        if (hp > 0) return;
        if (refillHP) stats.currentHp = stats.maxHp;
        diedEvent?.Raise();
        //currentPanel = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}