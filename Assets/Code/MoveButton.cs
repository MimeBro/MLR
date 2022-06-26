using System;
using DG.Tweening;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    private RectTransform _rectTransform;
    public MovesSO move;
    public Image buttonIcon;
    public MoveSet usedMovesSet;
    public GameEvent spellUsedEvent;
    public GameEvent notEnoughEnergyEvent;

    public MovesSO previousMove;

    private PlayerController player;
    
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
        if (GameManager.Instance.energy < move.energyCost)
        {
            notEnoughEnergyEvent?.Raise();
            return;
        }
        player.AddCommand(PerformMove, move.moveDuration);
       
    }

    public void PerformMove()
    {
        GameManager.Instance.UseEnergy(move.energyCost);
        Instantiate(move.moveGameObject, player.shootPoint.position, Quaternion.identity);
        spellUsedEvent?.Raise();
        usedMovesSet?.AddMove(move);
        Destroy(gameObject);
    }
}
