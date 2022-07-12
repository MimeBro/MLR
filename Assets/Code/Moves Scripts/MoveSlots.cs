using UnityEngine;

public class MoveSlots : MonoBehaviour
{
    public MoveButton setButton;
    public KeyCode assignedKey;
    public bool disabled;
    
    private void Update()
    {
        if(transform.childCount <= 0) return;
        
        if(setButton == null)
            setButton = GetComponentInChildren<MoveButton>();

        if (Input.GetKeyDown(assignedKey) && !disabled)
        {
            setButton?.CastMove();
        }
    }
}
