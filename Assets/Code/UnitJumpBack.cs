using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// Makes an unit jump back a panel, needs to be attached to an AAttack component.
/// </summary>
public class UnitJumpBack : MonoBehaviour
{
    public float jumpDelay;
    public float jumpDuration;
    public float jumpPower;

    private void Start()
    {
        JumpBack();
    }
    
    private async void JumpBack()
    {
        if (gameObject.TryGetComponent(out AAttack attack))
        {
             if (attack.attacker.gameObject.TryGetComponent(out UnitMovement unitMovement))
             {
                 var end = Time.time + jumpDelay;
                 while (Time.time < end)
                 {
                     await Task.Yield();
                 }
                 
                 unitMovement.JumpBack(jumpDuration, jumpPower);
             }
        }
    }
} 
