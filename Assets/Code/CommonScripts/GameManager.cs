using System.Collections.Generic;
using Code.CharacterScripts;
using Code.MonsterScripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.CommonScripts
{
    public class GameManager : MonoBehaviour
    {
        #region VARIABLES
        [Title("Player Team")]
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
        
        //Adds the enemies to be spawn from the map node
        public void AddEnemy(Enemy enemy)
        {
            EnemiesToSpawn.Add(enemy);
        }

        public void EndBattle()
        {

            
        }

    }
}
