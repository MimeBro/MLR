using System.Collections;
using System.Collections.Generic;
using Code.MoveScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "Monster/Stats")]
public class MonsterStats : ScriptableObject
{
    public int currentHp;
    public List<Moves> learnedMoves;

    public void UpdateHp(int amount)
    {
        currentHp = amount;
    }

    public void AddMove()
    {
        
    }

    public void ReplaceMove()
    {
        
    }

    public void RemoveMove()
    {
        
    }
    
}
