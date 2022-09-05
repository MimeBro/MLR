using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuidedProjectiles : AttackController
{
    [Title("Guided Projectiles")] public GuidedProjectile guidedProjectile;
    public float guidedProjectileSpeed;
    public float gapBetweenSpawn;
    public bool useAttackersShootPoint;
    public int amountOfShots;
    public Transform target;
    public override async Task CastAttack()
    {
        Debug.Log("Cast Attack called");
        var end = Time.time + startDelay;
        while (Time.time < end)await Task.Yield();

        
        await GuidedProjectile();
        gameObject.SetActive(false);
    }

    private async Task GuidedProjectile()
    {
        Debug.Log("Guided Projectile called");
        for (int j = 0; j < amountOfShots; j++)
        {
            await InstantiateGuidedProjectile();
        }
    }

    private async Task InstantiateGuidedProjectile()
    {
        if (!shootPositions.Any())
        {
            Debug.Log("InstantiateGuidedProjectile called with no shootpoints");
            var gp = Instantiate(guidedProjectile, transform.position, Quaternion.identity);
            gp.speed = guidedProjectileSpeed;
            gp.baseDamage = baseDamage;
            gp.target = target;
        }
        else
        {
            for (int i = 0; i < shootPositions.Count; i++)
            {
                Debug.Log("InstantiateGuidedProjectile called with shootpoints");

                var gp = Instantiate(guidedProjectile, shootPositions[i].position, Quaternion.identity);
                gp.speed = guidedProjectileSpeed;
                gp.baseDamage = baseDamage;
                gp.target = target;
            }
        }
        
        var end = Time.time + gapBetweenSpawn;
        while (Time.time < end) await Task.Yield();
    }
}