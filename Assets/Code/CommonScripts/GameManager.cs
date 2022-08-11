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
        public List<Monster> PlayersTeam = new List<Monster>(4);
        public Transform playerTeamTransform;
        
        //Monsters in the Enemy team
        public List<Monster> EnemyTeam = new List<Monster>();
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
            foreach (var monster in PlayersTeam)
            {
                monster.gameObject.SetActive(false);
            }
            
            ClearEnemies();
        }

        #region PLAYER TEAM MANAGEMENT
        
        //Capture a wild Monster
        public void CaptureMonster(Monster monster)
        {
            if (PlayersTeam.Count < 4)
            {
                var newMember = Instantiate(monster, playerTeamTransform);
                newMember.gameObject.SetActive(false);
                PlayersTeam.Add(newMember);
            }
        }

        public void RemoveMonster(int id)
        {
            Destroy(PlayersTeam[id].gameObject);
            PlayersTeam.Remove(PlayersTeam[id]);
        }
        
        public void ClearTeam()
        {
            foreach (var monster in PlayersTeam)
            {
                Destroy(monster.gameObject);
            }
            
            PlayersTeam.Clear();
        }
        
        #endregion

        #region ENEMY TEAM MANAGEMENT
        
        public void AddEnemy(Monster monster)
        {
            var newMember = Instantiate(monster, enemyTeamTransform);
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
