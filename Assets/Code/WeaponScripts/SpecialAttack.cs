using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    [CreateAssetMenu(fileName = "SpecialAttack", menuName = "Weapons/Special Attack")]
    public class SpecialAttack : ScriptableObject
    {
        [EnumToggleButtons]
        public WeaponType CompatibleWeapons;
        public WeaponRarity weaponRarity;
        public string attackName;
        public string attackDescription;

        public int durabilityCost;
        public bool targeted;
        public bool aimed;
        public bool melee;
        public bool canHitWeakPoints;
        
        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public List<Vector3> damage;
        
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
        public List<Vector3> attackTimings;

    }
}