using System.Threading.Tasks;
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
    
    public override async void CastAttack()
    {
        base.CastAttack();
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
            var sP = Instantiate(projectile, attacker.shootPoint.position, Quaternion.identity);
            sP.projectileSpeed = projectileSpeed;
            sP.attacker = attacker;
            sP.side = attacker.side;
            sP.shootPoint = attacker.shootPoint;
            sP.baseDamage = baseDamage;
            sP.attackDirection = attackDirection;
        }
        else
        {
            for (int i = 0; i < amountOfShots; i++)
            {
                var sP = Instantiate(projectile, attacker.shootPoint.position, Quaternion.identity);
                sP.projectileSpeed = projectileSpeed;
                sP.attacker = attacker;
                sP.side = attacker.side;
                sP.shootPoint = attacker.shootPoint;
                sP.baseDamage = baseDamage;
                sP.attackDirection = attackDirection;
            }
        }

    }
}
