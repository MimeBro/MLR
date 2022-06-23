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
    
    // Start is called before the first frame update
    private void Start()
    {
        transform.DOLocalMove(Vector3.zero, 0.2f);
        buttonIcon.sprite = move.moveIcon;
    }

    public void CastMove()
    {
        if (GameManager.Instance.energy < move.energyCost)
        {
            notEnoughEnergyEvent?.Raise();
            return;
        }
        GameManager.Instance.UseEnergy(move.energyCost);
        Instantiate(move.moveGameObject);
        spellUsedEvent?.Raise();
        usedMovesSet?.AddMove(move);
        Destroy(gameObject);
    }
}
