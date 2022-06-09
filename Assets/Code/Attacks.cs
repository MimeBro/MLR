using UnityEngine;

public class Attacks : MonoBehaviour
{
    public int damage;
    public Sides side;
    public bool destroySelf,destroyParent;
    public bool pierceThrough;
    

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Unit>())
        {
            Unit u;

            u = col.GetComponent<Unit>();
            if(u.side == side) return;

            if (u.uState == UnitState.DODGING)
            {
                return;
            }
            else
            {
                u.TakeDamage(damage);
                if(!pierceThrough)Destroy(gameObject);
            }
        }
    }
}