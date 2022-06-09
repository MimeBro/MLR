using UnityEngine;

public enum UnitState{STANDING, DODGING}
public class Unit : MonoBehaviour
{
    public Sides side;
    public int hp;
    public int maxhp;
    public Panel currentPanel;
    public UnitStatus unitStatus;
    public UnitState uState;
    
    private void Start()
    {
        maxhp = 100;
        hp = maxhp;
    }

    public void TakeDamage(int d)
    {
        Debug.Log(d);
        hp -= d;
    }

    public void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxhp);
        
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died");
        //currentPanel = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Panel>())
        {
            if (other.GetComponent<Panel>().occupier == this)
            {
                currentPanel = other.GetComponent<Panel>();
            }
        }
    } 
}