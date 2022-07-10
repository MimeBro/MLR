using System;
using DG.Tweening;
using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
    [Title("Moves")] public MoveSet moveSet;

    [Title("Other")] public Panel currentPanel;
    public UnitStatus unitStatus;
    public UnitState uState;
    public MMFeedbacks DamageFeedback;
    public GameEvent takeDamageEvent;

    public Vector2 boxSize;
    public LayerMask panelLayerMask;

    [Title("Setup")] public float yposition;
    public Transform shootPoint;
    public GameEvent diedEvent;
    public GameEvent monsterEntered;

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

    public void TakeDamage(int damage)
    {
        DamageFeedback?.PlayFeedbacks(transform.position, damage);

        stats.currentHp -= damage;
        takeDamageEvent?.Raise();
    }

    public void TakeDamage(int damage, ContactType cType, ElementalTypes eType)
    {
        var finalDamage = cType switch
        {
            ContactType.Physical => Mathf.FloorToInt(damage * ElementalInteractions.ElementalInteraction(eType, stats.primaryType) - stats.defense),
            ContactType.Special => Mathf.FloorToInt(damage * ElementalInteractions.ElementalInteraction(eType, stats.primaryType) - stats.specialDefense),
            _ => Mathf.FloorToInt(damage * ElementalInteractions.ElementalInteraction(eType, stats.primaryType))
        };
        
        DamageFeedback?.PlayFeedbacks(transform.position, finalDamage);
        if (finalDamage < 1) finalDamage = 1;
        stats.currentHp -= finalDamage;
        takeDamageEvent?.Raise();
    }

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
        var box = Physics2D.OverlapBox(transform.position, boxSize, 0, panelLayerMask);
        currentPanel = box?.GetComponent<Panel>();
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