using System;
using Code.WeaponScripts;
using TMPro;
using UnityEngine;

namespace Code.UI_scripts
{
    public class WeaponButton : MonoBehaviour
    {
        public Weapon assignedWeapon;
        public TextMeshProUGUI buttonText;
<<<<<<< HEAD
        [SerializeField]private WeaponAttack _weaponAttack;
=======
        public int assignedWeaponIndex;
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)

        private void OnEnable()
        {
            if (assignedWeapon != null) buttonText.text = assignedWeapon.weaponName;
        }

        public void UseWeapon()
        {
<<<<<<< HEAD
            _weaponAttack = BattleManager.Instance.GetPlayer().weaponAttack;
            _weaponAttack.weapon = assignedWeapon;
            if (assignedWeapon.targeted && !_selectingEnemies)
            {
                _selectingEnemies = true;
                return;
            }
            _weaponAttack.CastAttack();
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
                _weaponAttack.CastAttack(BattleManager.Instance.GetEnemy(_selectEnemyIndex));
                _playerActions.HideActions();
                _playerActions.HideWeapons();
                _selectingEnemies = false;
                
            }
=======
            Debug.Log(assignedWeapon.name + " selected");
            BattleManager.Instance.GetPlayer().acquiredWeapons[assignedWeaponIndex].CastAttack();
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
        }
    }
}