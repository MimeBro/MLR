using System.Linq;
using System.Threading.Tasks;
using Code.CommonScripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuidedProjectiles : AttackController
{
    [Title("Guided Projectiles")] 
    public GuidedProjectile guidedProjectile;
    public float guidedProjectileSpeed;
    public float gapBetweenSpawn;
    public int amountOfShots;
    public Transform target;
    public override async Task CastAttack(Unit attacker)
    {
        base.CastAttack(attacker);
        base.attacker = attacker;
        var end = Time.time + startDelay;
        while (Time.time < end) await Task.Yield();
        
        await GuidedProjectile();
        gameObject.SetActive(false);
    }

    private async Task GuidedProjectile()
    {
        for (int j = 0; j < amountOfShots; j++)
        {
            await InstantiateGuidedProjectile();
        }
    }

    private async Task InstantiateGuidedProjectile()
    {
        if (!shootPositions.Any())
        {
            var gp = Instantiate(guidedProjectile, transform.position, Quaternion.identity);
            gp.speed = guidedProjectileSpeed;
            gp.baseDamage = baseDamage;
            gp.target = target;
            gp.attacker = attacker;
        }
        else
        {
            for (int i = 0; i < shootPositions.Count; i++)
            {
                var gp = Instantiate(guidedProjectile, shootPositions[i].position, Quaternion.identity);
                gp.speed = guidedProjectileSpeed;
                gp.baseDamage = baseDamage;
                gp.target = target;
                gp.attacker = attacker;
            }
        }
        
        var end = Time.time + gapBetweenSpawn;
        while (Time.time < end) await Task.Yield();
    }
}