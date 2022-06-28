using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MoveSlotsManager : MonoBehaviour
{
    public List<MoveSlots> moveSlots;

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
