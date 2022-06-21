using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class MoveDraw : MonoBehaviour
{
    public GameObject moveToDraw;
    public MovesSlotsUI slots;
    
    public List<RectTransform> availableSlots = new List<RectTransform>();


    public void DrawAMove()
    {
        CheckForAvailableSlot();
        if (availableSlots.Any())
        {
            var drawnMove = Instantiate(moveToDraw, transform.position, quaternion.identity);
            drawnMove.transform.SetParent(availableSlots[0]);
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
}
