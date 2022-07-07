using DG.Tweening;
using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public enum UnitState{STANDING, DODGING}
public class Unit : MonoBehaviour
{
    [Title("Stats")]
    public MonsterSO stats;
    [EnumToggleButtons]
    public Sides side;
    [ProgressBar(0, "maxhp", Height = 30)]
    public int hp;
    [HideInInspector]
    public int maxhp;
    [ProgressBar(0, "maxEnergy", Segmented = true)]
    public float energy;
    [HideInInspector]
    public int maxEnergy;

    [Title("Moves")] 
    public MoveSet moveSet;
    
    [Title("Other")]
    public Panel currentPanel;
    public UnitStatus unitStatus;
    public UnitState uState;
    public MMFeedbacks DamageFeedback;
    public GameEvent takeDamageEvent;

    public Vector2 boxSize;
    public LayerMask panelLayerMask;
    
    [Title("Setup")]
    public float yposition;
    public Transform shootPoint;
    public GameEvent diedEvent;
    public GameEvent monsterEntered;
    
    [Title("Test")]
    public bool refillHP;
    public bool infiniteEnergy;
    
    private void Start()
    {   
        SetStats();
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

        Die();
        CheckPanel();
        EnergyCharge();
    }
    
    public void UseEnergy(int amount)
    {
        energy -= amount;
    }
    
    public void EnergyCharge()
    {
        if(side != Sides.PLAYER) return;
        energy = Mathf.Clamp(energy, 0, maxEnergy);

        if (energy < maxEnergy)
        {
            energy += stats.energyRegen * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        DamageFeedback?.PlayFeedbacks(transform.position, damage);

            stats.currentHp -= damage;
            takeDamageEvent?.Raise();
    }

    public void Heal(int healAmount)
    {
        hp += healAmount;
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
        transform.DOJump(destination,5,1,1).SetEase(Ease.Linear);
        monsterEntered?.Raise();
    }

    public void SwitchedOut()
    {
        var exitPos = TeamManager.Instance.MonsterSpawnPoint.position;
        transform.DOJump(exitPos,5,1,1).SetEase(Ease.Linear);
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