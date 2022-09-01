using System;
using System.Collections.Generic;
using System.Linq;
using Code.MonsterScripts;
using UnityEngine;

namespace Code.EnemyScripts
{
    public class Bandit : Enemy
    {
        public List<Enemy> assistants = new List<Enemy>();
        public override void StartMyTurn()
        {
            Bandit[] foundAsisstants;
            foundAsisstants = FindObjectsOfType<Bandit>();
            foreach (var asisstant in foundAsisstants)
            {
                if (asisstant != this)
                {
                    assistants.Add(asisstant);
                }
            }

            foreach (var assistant in assistants)
            {
                assistant.JoinAttack();
            }
            
            Attack();
        }

        public override void EndMyTurn()
        {
            base.EndMyTurn();
            
            if (assistants.Any())
            {
                assistants.Clear();
            }
        }
    }
}
