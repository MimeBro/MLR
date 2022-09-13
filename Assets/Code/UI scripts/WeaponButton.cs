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
        public int assignedWeaponIndex;

        private void OnEnable()
        {
            if (assignedWeapon != null) buttonText.text = assignedWeapon.weaponName;
        }

        public void UseWeapon()
        {
            Debug.Log(assignedWeapon.name + " selected");
            BattleManager.Instance.GetPlayer().acquiredWeapons[assignedWeaponIndex].CastAttack();
        }
    }
}