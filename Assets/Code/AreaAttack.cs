using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public int damagePerHit;
    public float secondsPerHit;
    private float firerate;
    private float _nextfire;

    public bool singleHit;
    private bool alreadyHit = false;

    public Sides side;

    private void OnTriggerStay2D(Collider2D other)
    {
        var unit = other.GetComponent<Unit>();
        firerate = secondsPerHit / 1;
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
        if (Time.time >= _nextfire)
        {
            unit.TakeDamage(damagePerHit);
            _nextfire = Time.time + firerate;
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
