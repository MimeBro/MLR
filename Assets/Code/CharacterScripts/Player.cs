using System;
using System.Collections.Generic;
using System.Linq;
using Code.CommonScripts;
using Code.WeaponScripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.CharacterScripts
{
    public class Player : Unit
    {
        #region VARIABLES
        
        [Title("Weapons")]
        [EnumToggleButtons]
        [Title("Compatible Weapons", Bold = false)]
        [HideLabel]
        public WeaponType compatibleWeapons;

        public WeaponAttack weaponAttack;
        public List<Weapon> acquiredWeapons = new List<Weapon>(3);
        public PlayerMovement _playerMovement;
        
        #endregion

        #region TURN MECHANICS

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void Update()
        {
<<<<<<< HEAD
            if (!_playerMovement.CanMove) return;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, battlePosition.position.x - 12f, battlePosition.position.x + 12f),
                Mathf.Clamp(transform.position.y, battlePosition.position.y - 12f, battlePosition.position.y + 12f),
                transform.position.z);
=======
            if (defaultWeapon != null)
            {
                var dweapon = Instantiate(defaultWeapon, weaponsTransform);
                dweapon.player = this;
                acquiredWeapons.Add(dweapon);
            }
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
        }

        public void GetWeapon(Weapon newWeapon)
        {
            if (acquiredWeapons.Count < 4)
            {

                acquiredWeapons.Add(newWeapon);
            }
            else
            {
                //Replace an existing weapon for the new one
            }
        }

        public async void StartMyTurn()
        {
            await _playerMovement.MoveToPosition(battlePosition.position);
        }

        public async void EndAttack()
        {
            await _playerMovement.MoveToPosition(battlePosition.position);
            BattleManager.Instance.SwitchTurn();
        }

        public async void EndMyTurn()
        {
            _playerMovement.CanMove = true;
        }

        #endregion
    }
}