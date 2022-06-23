using UnityEngine;

public class Attacks : MonoBehaviour
{
    public int damage;
    public Sides side;
    public bool destroySelf,destroyParent,pierceThrough,dodgeable;

    public virtual void Start()
    {
        if (destroySelf)
        {
            Destroy(gameObject, 5);
        }

        if (destroyParent)
        {
            Destroy(transform.parent.gameObject, 5);
        }
    }

    public void SetSide(Sides s)
    {
        side = s;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var unit = col.GetComponent<Unit>();
        if (unit != null)
        {
            if(unit.side == side) return;

            if (unit.uState == UnitState.DODGING && dodgeable)
            {
                return;
            }

            unit.TakeDamage(damage);
            if(!pierceThrough)Destroy(gameObject);
        }
    }
}