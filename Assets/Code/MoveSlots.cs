using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlots : MonoBehaviour
{
    public MoveButton setButton;
    public KeyCode assignedKey;

    
    private void Update()
    {
        if(transform.childCount <= 0) return;
        
        if(setButton == null)
            setButton = GetComponentInChildren<MoveButton>();

        if (Input.GetKeyDown(assignedKey))
        {
            setButton?.CastMove();
        }
    }
}
