using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.CommonScripts;
using Code.MonsterScripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.EnemyScripts
{
    public class Bandit : Enemy
    {
        [Title("Bandit Attributes")]
        public List<Enemy> assistants = new List<Enemy>();
        public GuidedProjectiles fireballAttack;
        
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
            Attack();
        }

        protected override void Attack()
        {
            if (assistants.Any())
            {
                foreach (var assistant in assistants)
                {
                    assistant.JoinAttack(this);
                }
                transform.DOMoveX(0, 0.5f).OnComplete(FireballAttack);
               
            }
            else
            {
               FireballAttack();
            }
        }

        private async void FireballAttack()
        {
            Debug.Log("Fireball Called");
            if (fireballAttack == null) return;
            
            fireballAttack.amountOfShots = 3;
            fireballAttack.gapBetweenSpawn = 1;
            fireballAttack.target = GameManager.Instance.playerCharacter.transform;
            
            await fireballAttack.CastAttack();
            transform.DOMoveX(returnPosition.position.x, 0.5f);
        }
        

        public override void JoinAttack(Enemy leader)
        {
            
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
