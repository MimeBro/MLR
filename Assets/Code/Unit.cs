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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Panel>())
        {
            currentPanel = other.GetComponent<Panel>();
        }
    } 
}