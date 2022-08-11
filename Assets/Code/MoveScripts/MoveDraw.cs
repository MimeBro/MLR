using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MoveDraw : MonoBehaviour
{
    public MoveButton moveButtonPrefab;
    public MoveSlotsManager slots;
    
    public List<MoveSlots> availableSlots = new List<MoveSlots>();

    public MoveSet DrawnMoves;
    public MoveSet UsedMoves;

    private MoveSet playerSet;

    private void Start()
    {
        DrawnMoves.Moves.Clear();
        UsedMoves.Moves.Clear();
        //playerSet = GameManager.Instance.playerMoveSet;
        ShuffleSet();
    }

    public void ShuffleSet()
    {
        for (int i = 0; i < playerSet.Moves.Count; i++)
        {
            DrawnMoves.AddMove(playerSet.Moves[i]);
        }
        
        DrawnMoves.Moves.Shuffle();
        FillSlots();
    }
    
    public void FillSlots()
    {
        CheckForAvailableSlot();
        for (int i = 0; i < availableSlots.Count; i++)
        {
            var mb = Instantiate(moveButtonPrefab, transform.position, Quaternion.identity);
            mb.transform.SetParent(availableSlots[i].transform);
            mb.SetMove(DrawnMoves.Moves[0]);
            UsedMoves.AddMove(DrawnMoves.Moves[0]);
            DrawnMoves.RemoveMove(0);
        }
    }
    
    public async void DrawAMove()
    {
        if (DrawnMoves.Moves.Count < availableSlots.Count)
        {
            RefillMoves();
        }
        var end = Time.time + 1f;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        
        CheckForAvailableSlot();
        
        if (availableSlots.Any())
        {
            var mb = Instantiate(moveButtonPrefab, transform.position, Quaternion.identity);
            mb.SetMove(DrawnMoves.Moves[0]);
            UsedMoves.Moves.Add(DrawnMoves.Moves[0]);
            DrawnMoves.RemoveMove(0);
            mb.transform.SetParent(availableSlots[0].transform);
        }
    }

    public void CheckForAvailableSlot()
    {
        availableSlots.Clear();
        
        for (int i = 0; i < slots.moveSlots.Count; i++)
        {
            if (slots.moveSlots[i].transform.childCount <= 0)
            {
                availableSlots.Add(slots.moveSlots[i]);
            }
            
            //await Task.Yield();
        }
    }

    private void Update()
    {
        
    }

    public void RefillMoves()
    {
        for (int i = 0; i < UsedMoves.Moves.Count; i++)
        {
            DrawnMoves.AddMove(UsedMoves.Moves[0]);
        }
        DrawnMoves.Moves.Shuffle();
        UsedMoves.Moves.Clear();
    }
    
    
}
