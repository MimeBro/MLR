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
        private PlayerMovement _playerMovement;
        
        #endregion

        #region TURN MECHANICS

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, returnPosition.position.x - 12f, returnPosition.position.x + 12f),
                Mathf.Clamp(transform.position.y, returnPosition.position.y - 12f, returnPosition.position.y + 12f),
                transform.position.z);
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

        public override void StartTurn()
        {
            _playerMovement.CanMove = false;
        }

        public void EndAttack()
        {
            BattleManager.Instance.SwitchTurn();
        }

        public override void EndMyTurn()
        {
            _playerMovement.CanMove = true;
        }

        #endregion
    }
}