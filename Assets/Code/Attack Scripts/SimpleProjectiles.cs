using System.Threading.Tasks;
using Code.CommonScripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleProjectiles : AttackController
{
    [Title("Simple Projectile")] 
    public SimpleProjectile projectile;
    public int amountOfShots;
    
    public float gapBetweenShot;
    public bool useAttackersShootPoint;

    public float projectileSpeed;
    
    public override async Task CastAttack(Unit attacker)
    {
        base.CastAttack(attacker);
        var end = Time.time + startDelay;
        while (Time.time < end)await Task.Yield();
        
        await SimpleProjectile();
        gameObject.SetActive(false);
    }

    public async Task SimpleProjectile()
    {
        for (int i = 0; i < amountOfShots; i++)
        {
            await InstantiateSimpleProjectile();
        }
    }

    public async Task InstantiateSimpleProjectile()
    {
        var end = gapBetweenShot + Time.time;
        while (Time.time < end) await Task.Yield();

        if (useAttackersShootPoint)
        {
            var sP = Instantiate(projectile, transform.position, Quaternion.identity);
            sP.projectileSpeed = projectileSpeed;
            sP.baseDamage = baseDamage;
            sP.attackDirection = attackDirection;
        }
        else
        {
            for (int i = 0; i < amountOfShots; i++)
            {
                var sP = Instantiate(projectile, transform.position, Quaternion.identity);
                sP.projectileSpeed = projectileSpeed;
                sP.baseDamage = baseDamage;
                sP.attackDirection = attackDirection;
            }
        }

    }
}
