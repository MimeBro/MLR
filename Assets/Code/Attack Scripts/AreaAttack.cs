using Sirenix.OdinInspector;
using UnityEngine;

public class AreaAttack : Attacks
{
    [Title("Area Attack")]
    public int damagePerHit;
    public float secondsPerHit;
    
    private float fireRate;
    private float nextFire;

    public bool singleHit;
    private bool alreadyHit = false;

    public override void OnTriggerEnter2D(Collider2D col)
    {
        //do Nothing
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var unit = other.GetComponent<Unit>();
        fireRate = secondsPerHit / 1;
        if (unit != null)
        {
            if (unit.side == side) return;
            
            if (singleHit)
            {
                SingleHit(unit);
                return;
            }
            MultiHit(unit);
        }
    }

    public void MultiHit(Unit unit)
    {
        if (Time.time >= nextFire)
        {
            unit.TakeDamage(damagePerHit);
            nextFire = Time.time + fireRate;
        }
    }

    public void SingleHit(Unit unit)
    {
        if (!alreadyHit)
        {
            unit.TakeDamage(damagePerHit);
            alreadyHit = true;
        }
    }
}
