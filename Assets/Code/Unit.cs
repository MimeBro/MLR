using System;
using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public enum UnitState{STANDING, DODGING}
public class Unit : MonoBehaviour
{
    [Header("Stats")]
    public Sides side;
    public int hp;
    public int maxhp;

    [Header("Other")]
    public Panel currentPanel;
    public UnitStatus unitStatus;
    public UnitState uState;
    public MMFeedbacks DamageFeedback;
    public GameEvent takeDamageEvent;

    public Vector2 boxSize;
    public LayerMask panelLayerMask;

    [Header("Test")] 
    public bool refillHP;
    public bool infiniteEnergy;
    
    private void Start()
    {
        hp = maxhp;
    }

    public void TakeDamage(int damage)
    {
        DamageFeedback?.PlayFeedbacks(transform.position, damage);

            hp -= damage;
            takeDamageEvent?.Raise();
    }

    public void Heal(int healAmount)
    {
        hp += healAmount;
    }

    public void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxhp);
        
        if (hp <= 0)
        {
            Die();
        }
        CheckPanel();
    }

    void CheckPanel()
    {
        var box = Physics2D.OverlapBox(transform.position, boxSize, 0, panelLayerMask);
        currentPanel = box?.GetComponent<Panel>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    private void Die()
    {
        if (refillHP) hp = maxhp;
        Debug.Log("Died");
        //currentPanel = null;
    }
}