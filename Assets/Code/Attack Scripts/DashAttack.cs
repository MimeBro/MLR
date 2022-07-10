using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DashAttack : Attacks
{
    [Title("Setup")]
    public Vector3 startPanel;
    public Panel endPanel;

    [Title("Values")]
    [EnumToggleButtons]
    public AttackDirection direction;

    public float dashSpeed;
    public float dashDuration;
    private float dashDur;
    
    public bool pierce;

    public void Dash()
    {
        attacker = TeamManager.Instance.GetPlayer();//FOR TESTING, THE PARENT SCRIPT SHOULD GIVE IT THE CORRECT ATTACKER
        
        startPanel = attacker.transform.position;
        transform.parent = attacker.transform;
        transform.localPosition = Vector3.zero;
        attacker.transform.DOMoveX(endPanel.transform.position.x, dashSpeed).OnComplete(ReturnBack);
    }
    

    public void InterruptReturn()
    {
        dashDur++;
        dashDur = Mathf.Clamp(dashDur, 0, 1);
    }

    public void InterruptReturn(float amount)
    {
        dashDur += amount;
    }

    public async void ReturnBack()
    {
        dashDur = dashDuration;
        while (dashDur > 0)
        {
            dashDur -= 1 * Time.deltaTime;
            await Task.Yield();
        }

        attacker.transform.position = startPanel;
    }
}