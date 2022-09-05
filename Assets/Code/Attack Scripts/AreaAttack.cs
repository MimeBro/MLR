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
        var unit = other.GetComponent<OldUnit>();
        fireRate = secondsPerHit / 1;
        if (unit != null)
        {
            if (singleHit)
            {
                SingleHit(unit);
                return;
            }
            MultiHit(unit);
        }
    }

    public void MultiHit(OldUnit oldUnit)
    {
        if (Time.time >= nextFire)
        {
            oldUnit.TakeDamage(damagePerHit);
            nextFire = Time.time + fireRate;
        }
    }

    public void SingleHit(OldUnit oldUnit)
    {
        if (!alreadyHit)
        {
            oldUnit.TakeDamage(damagePerHit);
            alreadyHit = true;
        }
    }
}
