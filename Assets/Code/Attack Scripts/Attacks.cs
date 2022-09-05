using System;
using Code.CommonScripts;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public enum ContactType{Physical, Special}
public class Attacks : MonoBehaviour
{
    [Title("General Attack Properties")]
    [EnumToggleButtons]
    public Sides side;

    public Unit attacker;
    
    public int baseDamage;

    public GameObject hitVfx;
    
    public bool destroySelf,destroyParent,pierceThrough,dodgeable;
    


    public virtual void Start()
    {
        
    }

    public int FinalDamage()
    {
        return 0;
    }
    
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        var unit = col.GetComponent<Unit>();
        if (unit != null)
        {
            unit.TakeDamage(FinalDamage());
            
            if (hitVfx != null)
            {
                var vfx = Instantiate(hitVfx, unit.transform.position, quaternion.identity);
                Destroy(vfx,3);
            }

            if(!pierceThrough) Destroy(gameObject);
        }
    }
}