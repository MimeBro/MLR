using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSet.asset", menuName = ("Moves/Set"))]
public class MoveSet : ScriptableObject
{
    public List<MovesSO> Moves;
    public MoveSet defaultMoveSet;

    public void AddMove(MovesSO move)
    {
        Moves.Add(move);
    }

    public void RemoveMove(int index)
    {
        Moves.Remove(Moves[index]);
    }

    public void ResetDeck()
    {
        if(defaultMoveSet == null) return;
        
        Moves.Clear();
        
        for (int i = 0; i < defaultMoveSet.Moves.Count; i++)
        {
            Moves.Add(defaultMoveSet.Moves[i]);
        }
    }
}
