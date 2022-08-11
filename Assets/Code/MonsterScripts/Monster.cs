using System;
using System.Collections.Generic;
using Code.CommonScripts;
using Code.MoveScripts;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.MonsterScripts
{
    public class Monster : MonoBehaviour
    {
        #region VARIABLES

        [Title("Monster Info")] 
        //Id of this monster for the Game Manager
        public int ID;
        
        public string monstersName;
        
        public bool nicknamed;
        [ShowIf("nicknamed")] 
        public string monstersNickname;
        
        [TextArea]
        public string MonstersDescription;
        
        [Title("Evolution")]
        public bool evolves;
        [ShowIf("evolves")] 
        public EvolutionStage evolutionStage;
        //The monster this monster evolves into
        [ShowIf("evolves")]
        public Monster evolution;
        
        [Title("Monster Stats")]
        [EnumPaging] public ElementalTypes elementalType;
            
        [PropertySpace]
        [ProgressBar(0,"maxHp", Height = 30)]
        public int currentHp;
        public int maxHp;
            
        [PropertySpace]
        public int attack;
        
        [PropertySpace]
        public int defense;
            
        [PropertySpace]
        public int speed;
        
        [Title("Moves")]
        //List of moves the monster has already learned, maximum 4
        public List<Moves> LearnedMoves = new List<Moves>(4);
        //List of moves the monster can learn
        public List<Moves> CompatibleMoves = new List<Moves>();

        [Title("Feedbacks")]
        public MMFeedbacks takeDamageFeedback;
        
        [HideInInspector]public bool myTurn;
  
        
        #endregion

        #region TURN MECHANICS

        public void StartTurn()
        {
            myTurn = true;
        }

        public void EndTurn()
        {
            myTurn = false;
        }

        #endregion

        #region DAMAGE CALCULATIONS

        //Take damage without any calculations
        public void TakeDamage(int amount)
        {
            currentHp -= amount;
            takeDamageFeedback?.PlayFeedbacks();
        }
        
        //Take damage and multiply it by different variables
        public void TakeDamage(int damage, ContactType contactType, ElementalTypes elementalTypes)
        {
            var finalDamage =
                Mathf.FloorToInt(damage * ElementalInteractions.ElementalInteraction(elementalTypes, elementalType));
            currentHp -= finalDamage - defense;
            takeDamageFeedback?.PlayFeedbacks();
        }
        
        #endregion

        #region MOVES
        
        //Learn a new Move
        public void LearnMove(Moves move)
        {
            LearnedMoves.Add(move);
        }
        
        //Remove a learned move
        public void ForgetMove(int moveIndex)
        {
            LearnedMoves.Remove(LearnedMoves[moveIndex]);
        }

        #endregion

        #region CAPTURE

        public void CaptureMe()
        {
            GameManager.Instance.CaptureMonster(this);
        }
        
        #endregion
    }
}