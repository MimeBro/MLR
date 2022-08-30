using System;
using System.Collections.Generic;
using Code.MonsterScripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Code.CommonScripts
{
    public class GameManager : MonoBehaviour
    {
        #region VARIABLES
        [FormerlySerializedAs("playerPlayer")] [Title("Player Team")]
        //Monsters in the Player team
        public Player playerCharacter;
        
        //Monsters in the Enemy team
        public List<Unit> EnemiesToSpawn = new List<Unit>();

        public static GameManager Instance;
        
        #endregion

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void AddEnemy(Enemy enemy)
        {
            EnemiesToSpawn.Add(enemy);
        }

        public void EndBattle()
        {

            
        }

    }
}
