using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DashAttack : Attacks
{
    [Title("Setup")]
    public Vector3 startPosition;
    public Panel targetPanel;

    [Title("Values")]
    [EnumToggleButtons]
    public AttackDirection direction;

    public bool backAndForth;
    public float dashSpeed;
    public float dashDuration;
    
    private float dashDur;


    public void StartDash()
    {
        startPosition = attacker.transform.position;
        transform.parent = attacker.transform;
        transform.localPosition = Vector3.zero;
        attacker.transform.DOMoveX(targetPanel.transform.position.x, dashSpeed).OnComplete(ReturnBack);
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
        
        if (backAndForth)
        {
            attacker.transform.DOMoveX(startPosition.x, dashSpeed).OnComplete(
                () =>
                {
                    Destroy(gameObject);
                });
            
        }
        
        else
        {
            attacker.transform.position = startPosition;
            Destroy(gameObject);
        }
    }
}