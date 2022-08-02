using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public enum ContactType{Physical, Special}
public class Attacks : MonoBehaviour
{
    [Title("General Attack Properties")]
    [EnumToggleButtons]
    public Sides side;

    public ElementalTypes element;
    public ContactType contactType;
    public Unit attacker;
    
    public int baseDamage;

    public GameObject hitVfx;
    
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

    public int FinalDamage()
    {
        return contactType switch
        {
            ContactType.Physical => baseDamage + attacker.stats.attack,
            ContactType.Special => baseDamage + attacker.stats.specialAttack,
            _ => baseDamage
        };
    }
    
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        var unit = col.GetComponent<Unit>();
        if (unit != null)
        {
            if(unit.side == side) return;

            if (unit.uState == UnitState.DODGING && dodgeable)
            {
                return;
            }

            unit.TakeDamage(FinalDamage(), contactType, element);
            
            if (hitVfx != null)
            {
                var vfx = Instantiate(hitVfx, unit.transform.position, quaternion.identity);
                Destroy(vfx,3);
            }

            if(!pierceThrough) Destroy(gameObject);
        }
    }
}