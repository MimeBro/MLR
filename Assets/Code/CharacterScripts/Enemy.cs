using Code.CommonScripts;
using UnityEngine;

namespace Code.MonsterScripts
{
    public class Enemy : Unit
    {
        public bool alreadyAttacked;

        public override void StartMyTurn()
        {
            Attack();
        }

        protected virtual void Attack()
        {
            //Do something
            Debug.Log(gameObject.name + " is attacking");
            EndMyTurn();
        }

        public virtual void JoinAttack()
        {
            //Do something
            Debug.Log(gameObject.name + " Joined another's attack");
            alreadyAttacked = true;
        }

        public override void EndMyTurn()
        {
            alreadyAttacked = true;
            //Turn end logic
            
            //tell battle manager to let the next enemy attack if any.
            BattleManager.Instance.NextTurn();
        }

        public void ResetAttackingStatus()
        {
            alreadyAttacked = false;
        }
    }
}