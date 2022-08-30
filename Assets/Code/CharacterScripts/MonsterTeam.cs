using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTeam.asset", menuName = "Monster/Team")]
public class MonsterTeam : ScriptableObject
    {
        public List<OldUnit> Monsters;

        public void AddMove(OldUnit monster)
        {
            Monsters.Add(monster);
        }

        public void RemoveMove(int index)
        {
            Monsters.Remove(Monsters[index]);
        }

        public void ResetDeck()
        {
            
        }
}
