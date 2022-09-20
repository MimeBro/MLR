using System.Collections.Generic;
using System.Linq;
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
        
        public override void StartTurn()
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
                //transform.DOMoveX(0, 0.5f).OnComplete(FireballAttack);
                FireballAttack();
               
            }
            
            else
            {
               FireballAttack();
            }
        }

        private async void FireballAttack()
        {
            if (fireballAttack == null) return;
            
            fireballAttack.target = GameManager.Instance.playerCharacter.transform;
            
            await fireballAttack.CastAttack(this);
            //transform.DOMoveX(returnPosition.position.x, 0.5f).OnComplete(EndMyTurn);
            EndTurn();
        }
        

        public override void JoinAttack(Enemy leader)
        {
            
        }

        public override void EndTurn()
        {
            base.EndTurn();
            
            if (assistants.Any())
            {
                assistants.Clear();
            }
        }
    }
}
