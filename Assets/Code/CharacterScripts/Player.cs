using System.Collections.Generic;
using Code.CommonScripts;
using Code.MoveScripts;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.MonsterScripts
{
    public class Player : Unit
    {
        #region VARIABLES

        public float currentEnergy;
        public int maxEnergy;
        
        [Title("Moves")]
        //List of moves the monster has already learned, maximum 4
        public List<Moves> LearnedMoves = new List<Moves>();
        //List of moves the monster can learn
        public List<Moves> CompatibleMoves = new List<Moves>();

        [Title("Feedbacks")]
        public MMFeedbacks takeDamageFeedback;
        
        [HideInInspector]public bool myTurn;

        private Rigidbody2D _rigidbody2D;

        #endregion

        #region TURN MECHANICS

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void StartTurn()
        {
            myTurn = true;
        }

        public void EndTurn()
        {
            myTurn = false;
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

    }
}