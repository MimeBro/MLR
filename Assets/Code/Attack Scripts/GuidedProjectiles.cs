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

    public override async void CastAttack()
    {
        base.CastAttack();
        var end = Time.time + startDelay;
        while (Time.time < end)await Task.Yield();

        
        await GuidedProjectile();
        gameObject.SetActive(false);
    }

    private async Task GuidedProjectile()
    {
        for (int j = 0; j < amountOfShots; j++)
        {
            if (hitAllPanelsInTheWay)
            {
                for (int i = 0; i < panels.Count; i++)
                {
                    await InstantiateGuidedProjectile(i);
                }

                return;
            }

            await InstantiateGuidedProjectile(panels.Count - 1);

            if (attackDirection == AttackDirection.Both)
            {
                await InstantiateGuidedProjectile(0);
            }
        }
    }

    private async Task InstantiateGuidedProjectile(int panelIndex)
    {

        if (useAttackersShootPoint)
        {
            var gp = Instantiate(guidedProjectile, attacker.shootPoint.position,
                Quaternion.identity);
            gp.target = panels[panelIndex].transform;
            gp.speed = guidedProjectileSpeed;
            gp.baseDamage = baseDamage;
            gp.attacker = attacker;
            gp.side = attacker.side;
            gp.element = moveType;
        }
        else
        {
            for (int i = 0; i < shootPositions.Count; i++)
            {
                var gp = Instantiate(guidedProjectile, shootPositions[i].position, Quaternion.identity);
                gp.target = panels[panelIndex].transform;
                gp.speed = guidedProjectileSpeed;
                gp.baseDamage = baseDamage;
                gp.attacker = attacker;
                gp.side = attacker.side;
                gp.element = moveType;
            }
        }
        
        var end = Time.time + gapBetweenSpawn;
        while (Time.time < end) await Task.Yield();
    }
}