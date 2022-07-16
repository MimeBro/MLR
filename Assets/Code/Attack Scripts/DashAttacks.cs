using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class DashAttacks : AttackController
{
    [Title("Dash Attack")] 
    public DashAttack dashAttack;
    public bool backAndForth;
    public float dashSpeed;
    public float dashDuration;

    public override async void CastAttack()
    {
        base.CastAttack();
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
        dA.attacker = attacker;
        dA.targetPanel = panels[panels.Count - 1];
        dA.backAndForth = backAndForth;
        dA.direction = attackDirection;
        dA.dashSpeed = dashSpeed;
        dA.dashDuration = dashDuration;
        dA.side = attacker.side;
        dA.StartDash();
        return Task.CompletedTask;
    }
}