using System;
using System.Threading.Tasks;
using DG.Tweening;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    private RectTransform _rectTransform;
    public MovesSO move;
    public Image buttonIcon;
    public GameEvent moveUsed;
    public GameEvent notEnoughEnergyEvent;

    public MovesSO previousMove;

    private bool waitingForMove;
    private bool waitForPosition;
    private AttackController activeMove;
    
    public void SetMove(MovesSO newMove)
    {
        if (activeMove != null) Destroy(activeMove.gameObject);

        move = newMove;
        buttonIcon.sprite = newMove.moveIcon;
        transform.DOLocalMove(Vector3.zero, 0.2f);
        activeMove = (Instantiate(move.attackController));
        activeMove.attacker = TeamManager.Instance.GetPlayer();
        activeMove.moveType = move.moveType;
        activeMove.gameObject.SetActive(false);
    }

    public async void CastOnDraw()
    {
        var end = Time.time + 0.5f;
        
        while (Time.time < end) await Task.Yield();
        
        PerformMove();
    }
    
    public void ChangeMove(MovesSO newMove)
    {
        if (move != null)
        {
            previousMove = move;
        }
        SetMove(newMove);
    }

    public void CastMove()
    {
        if(move.castOnDraw) return;
        
        //If there's no energy, return
        if (TeamManager.Instance.GetPlayer().energy < move.energyCost)
        {
            notEnoughEnergyEvent?.Raise();
            return;
        }
        
        PlayerController.Instance.AddCommand(PerformMove, activeMove.moveDuration);
    }

    public void PerformMove()
    {
        TeamManager.Instance.GetPlayer().UseEnergy(move.energyCost);
        activeMove.gameObject.SetActive(true);
        activeMove.CastAttack();
        GoOnCooldown();
    }

    public void WaitingForMoveToFinish()
    {

        //play Animation or whatever but don't get discarded yet

    }

    public void GoOnCooldown()
    {
        moveUsed?.Raise();
    }

    public void ConditionsMet()
    {
        Debug.Log("Conditions Met");
        waitingForMove = false;
        GoOnCooldown();
    }
}
