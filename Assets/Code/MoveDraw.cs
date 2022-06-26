using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MoveDraw : MonoBehaviour
{
    public MoveButton moveButtonPrefab;
    public MoveSlotsManager slots;
    
    public List<RectTransform> availableSlots = new List<RectTransform>();

    public MoveSet DrawnMoves;
    public MoveSet UsedMoves;

    private MoveSet playerSet;

    private void Start()
    {
        DrawnMoves.Moves.Clear();
        UsedMoves.Moves.Clear();
        playerSet = GameManager.Instance.playerMoveSet;
        ShuffleSet();
    }

    public void ShuffleSet()
    {
        foreach (var move in playerSet.Moves)
        {
            DrawnMoves.AddMove(move);
        }
        DrawnMoves.Moves.Shuffle();
        FillSlots();
    }

    public void FillSlots()
    {
        CheckForAvailableSlot();
        for (int i = 0; i < availableSlots.Count; i++)
        {
            var dm = Instantiate(moveButtonPrefab, transform.position, Quaternion.identity);
            dm.SetMove(DrawnMoves.Moves[0]);
            dm.usedMovesSet = UsedMoves;
            DrawnMoves.RemoveMove(0);
            dm.transform.SetParent(availableSlots[i]);
        }
    }
    
    public async void DrawAMove()
    {
        var end = Time.time + 1f;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        
        CheckForAvailableSlot();
        
        
        if (availableSlots.Any())
        {
            var dm = Instantiate(moveButtonPrefab, transform.position, Quaternion.identity);
            dm.SetMove(DrawnMoves.Moves[0]);
            dm.usedMovesSet = UsedMoves;
            DrawnMoves.RemoveMove(0);
            dm.transform.SetParent(availableSlots[0]);
        }
    }

    public void CheckForAvailableSlot()
    {
        availableSlots.Clear();
        
        for (int i = 0; i < slots.moveSlots.Count; i++)
        {
            if (slots.moveSlots[i].childCount <= 0)
            {
                availableSlots.Add(slots.moveSlots[i]);
            }
        }
    }

    private void Update()
    {
        if (DrawnMoves.Moves.Count == 0)
        {
            RefillMoves();
        }
    }

    public void RefillMoves()
    {
        
        for (int i = 0; i < UsedMoves.Moves.Count; i++)
        {
            DrawnMoves.AddMove(UsedMoves.Moves[i]);
            UsedMoves.RemoveMove(i);
        }
        DrawnMoves.Moves.Shuffle();
    }
    
    
}
