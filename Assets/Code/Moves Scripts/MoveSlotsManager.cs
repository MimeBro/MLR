using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveSlotsManager : MonoBehaviour
{
    public List<MoveSlots> moveSlots;

    public void SetMoves()
    {
        foreach (var slot in moveSlots)
        {
            slot.setButton.gameObject.SetActive(false);
            slot.disabled = true;
        }

        for (int i = 0; i < TeamManager.Instance.GetPlayerMoves().Moves.Count; i++)
        {
            moveSlots[i].setButton.gameObject.SetActive(true);
            moveSlots[i].disabled = false;
            moveSlots[i].setButton.SetMove(TeamManager.Instance.GetPlayerMoves().Moves[i]);
        }
    }

    public void DisableAllSlots()
    {
        foreach (var slot in moveSlots)
        {
            slot.disabled = true;
        }
    }

    public async void DisableAllSlots(float duration)
    {
        foreach (var slot in moveSlots)
        {
            slot.disabled = true;
        }

        var end = Time.time + duration;
        
        while (Time.time < end)
        {
           await Task.Yield();
        }
        EnableallSlots();
    }

    public void EnableallSlots()
    {
        foreach (var slot in moveSlots)
        {
            slot.disabled = false;
        }
    }
}
