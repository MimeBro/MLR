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

    private PlayerController player;
    private bool waitingForMove;
    private bool waitForPosition;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = PlayerController.Instance;
    }

    public void SetMove(MovesSO newMove)
    {
        move = newMove;
        buttonIcon.sprite = move.moveIcon;
        transform.DOLocalMove(Vector3.zero, 0.2f);
        if(move.castOnDraw) PerformMove();
        

    }

    public async void CastOnDraw()
    {
        var end = Time.time + 0.5f;
        
        while (Time.time < end)
        {
           await Task.Yield();
        }
        
        PerformMove();
    }
    
    public void ChangeMove(MovesSO newMove)
    {
        if (move != null)
        {
            previousMove = move;
        }

        move = newMove;
        buttonIcon.sprite = move.moveIcon;
    }

    public void CastMove()
    {
        if(move.castOnDraw) return;
        if (GameManager.Instance.energy < move.energyCost)
        {
            notEnoughEnergyEvent?.Raise();
            return;
        }
        player.AddCommand(PerformMove, move.MoveDuration());
    }

    public void PerformMove()
    {
        GameManager.Instance.UseEnergy(move.energyCost);
        var mov = Instantiate(move.moveGameObject);
        mov.callerButton = this;
        mov.CastAttack();
        if (move.waitTillAttackEnds)
        {
            waitingForMove = true;
            return;
        }
        
        DiscardSelf();
    }

    public void WaitingForMoveToFinish()
    {

        //play Animation or whatever but don't get discarded yet

    }

    public void DiscardSelf()
    {
        moveUsed?.Raise();
        Destroy(gameObject);
    }

    public void ConditionsMet()
    {
        Debug.Log("Conditions Met");
        waitingForMove = false;
        DiscardSelf();
    }
}
