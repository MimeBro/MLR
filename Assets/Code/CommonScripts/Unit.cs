using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Code.CommonScripts
{
    public class Unit : MonoBehaviour
    {
        [Title("Description")]
        public string characterName;
        [TextArea]
        public string characterDescription;
        
        [Title("Stats")] 
        public int currentHp;
        public int maxHp;
        
        public int speed;
        public int addedAttack;
        public int addedDefense;
        
        [Title("Other")]
        //Feedback to play when the unit takes damage.
        public MMFeedbacks takeDamageFeedback;
        //Position where the unit has to return to in case it's moved.
        public Transform returnPosition;

        public void TakeDamage(int dmg)
        {
            takeDamageFeedback?.PlayFeedbacks();
            currentHp -= dmg;
        }

        public void Heal(int amount)
        {
            currentHp += amount;
            currentHp = Mathf.Clamp(currentHp,0, maxHp);
        }

        public virtual void StartMyTurn()
        {
            //Start Turn logic
        }

        public virtual void EndMyTurn()
        {
            //End Turn Logic
        }

        public virtual void Die()
        {
            //Die event
        }
    }
}