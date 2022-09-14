using System;
using System.Collections.Generic;
using Code.MonsterScripts;
using Code.WeaponScripts;
using TMPro;
using UnityEngine;

namespace Code.UI_scripts
{
    public class WeaponButton : MonoBehaviour
    {
        public Weapon assignedWeapon;
        public TextMeshProUGUI buttonText;

        private bool _selectingEnemies;
        [SerializeField]private int _selectEnemyIndex;
        [HideInInspector] public PlayerActionsUI _playerActions;
        
        private void Update()
        {
            EnemySelection();
            SelectedEnemy();
        }

        public void UseWeapon()
        {
            Debug.Log(assignedWeapon.name + " selected");
            if (assignedWeapon.targeted && !_selectingEnemies)
            {
                _selectingEnemies = true;
                return;
            }
            assignedWeapon.CastAttack();
            _playerActions.HideActions();
            _playerActions.HideWeapons();
        }

        public void EnemySelection()
        {
            if(!_selectingEnemies) return;
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _selectEnemyIndex--;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _selectEnemyIndex++;
            }
            
            if (_selectEnemyIndex < 0)
            {
                _selectEnemyIndex = BattleManager.Instance.enemiesOnTheField.Count - 1;
            }

            if (_selectEnemyIndex >= BattleManager.Instance.enemiesOnTheField.Count)
            {
                _selectEnemyIndex = 0;
            }
        }

        public void SelectedEnemy()
        {
            if (Input.GetKeyDown(KeyCode.E) && _selectingEnemies)
            {
                assignedWeapon.CastAttack(BattleManager.Instance.GetEnemy(_selectEnemyIndex));
                _playerActions.HideActions();
                _playerActions.HideWeapons();
                _selectingEnemies = false;
                
            }
        }
    }
}