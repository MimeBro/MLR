using System;
using System.Collections.Generic;
using Code.MonsterScripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.CommonScripts
{
    public class GameManager : MonoBehaviour
    {
        #region VARIABLES

        [Title("Player Team")]
        //Monsters in the Player team
        public Character PlayerCharacter;
        public Transform playerTeamTransform;
        
        //Monsters in the Enemy team
        public List<Character> EnemyTeam = new List<Character>();
        public Transform enemyTeamTransform;

        public static GameManager Instance;
        
        #endregion

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void EndBattle()
        {

            
            ClearEnemies();
        }

        #region PLAYER MANAGEMENT
        
        #endregion

        #region ENEMY TEAM MANAGEMENT
        
        public void AddEnemy(Character character)
        {
            var newMember = Instantiate(character, enemyTeamTransform);
            newMember.gameObject.SetActive(false);
            EnemyTeam.Add(newMember);
        }

        public void ClearEnemies()
        {
            foreach (var monster in EnemyTeam)
            {
                Destroy(monster.gameObject);
            }
            
            EnemyTeam.Clear();
        }

        #endregion

        private void Start()
        {

        }

        private void Update()
        {

        }

    }
}
