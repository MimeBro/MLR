using System;
using Code.CharacterScripts;
using Code.CommonScripts;
using Code.MonsterScripts;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [Title("General Attack Properties")]
    [EnumToggleButtons]

    public Unit attacker;
    
    public int baseDamage;

    public GameObject hitVfx;
    
    public bool pierceThrough;
    


    public virtual void Start()
    {
        
    }

    public int FinalDamage()
    {
        return baseDamage + 0;
    }
    
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        //If the attacker is an enemy, only hit the player
        if (attacker.TryGetComponent(out Enemy enemy))
        {
            if (col.TryGetComponent(out Player plyr))
            {
                plyr.TakeDamage(FinalDamage());
                
                if (hitVfx != null)
                {
                    var vfx = Instantiate(hitVfx, col.transform.position, quaternion.identity);
                    Destroy(vfx,3);
                }

                if(!pierceThrough) Destroy(gameObject);
            }
        }
        
        //If the attacker is the player, only hit enemies
        else if (attacker.TryGetComponent(out Player player))
        {
            if (col.TryGetComponent(out Enemy enmy))
            {
                enmy.TakeDamage(FinalDamage());
                
                if (hitVfx != null)
                {
                    var vfx = Instantiate(hitVfx, col.transform.position, quaternion.identity);
                    Destroy(vfx,3);
                }

                if(!pierceThrough) Destroy(gameObject);
            }
        }

    }
}