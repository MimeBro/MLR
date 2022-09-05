using System.Threading.Tasks;
using Code.CommonScripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class DashAttacks : AttackController
{
    [Title("Dash Attack")] 
    public DashAttack dashAttack;
    public bool backAndForth;
    public float dashSpeed;
    public float dashDuration;

    public override async Task CastAttack(Unit attacker)
    {
        base.CastAttack(attacker);
        var end = Time.time + startDelay;
        
        while (Time.time < end)
        {
            await Task.Yield();
        }

        await DashAttack();
        gameObject.SetActive(false);
    }
    
    private Task DashAttack()
    {
        var dA = Instantiate(dashAttack);
        dA.backAndForth = backAndForth;
        dA.direction = attackDirection;
        dA.dashSpeed = dashSpeed;
        dA.dashDuration = dashDuration;
        dA.StartDash();
        return Task.CompletedTask;
    }
}