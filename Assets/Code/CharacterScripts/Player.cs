using System;
using System.Collections.Generic;
using Code.CommonScripts;
using Code.MoveScripts;
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
        public WeaponType CompatibleWeapons;

        public Weapon defaultWeapon;
        public List<Weapon> acquiredWeapons = new List<Weapon>(3);

        public Transform weaponsTransform;
        private PlayerMovement _playerMovement;
        
        #endregion

        #region TURN MECHANICS

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            if (defaultWeapon != null)
            {
                var dweapon = Instantiate(defaultWeapon, weaponsTransform);
                acquiredWeapons.Add(dweapon);
            }
        }

        public void GetWeapon(Weapon weapon)
        {
            if (acquiredWeapons.Count < 4)
            {
                acquiredWeapons.Add(weapon);
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

        public void EndTurn()
        {
            _playerMovement.CanMove = true;
        }

        #endregion
    }
}