using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
    public class Weapon : ScriptableObject
    {
        [EnumToggleButtons]
        public WeaponType WeaponType;
        public WeaponRarity weaponRarity;
        public string weaponName;
        [Title("Weapon Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string weaponDescription;

        public Vector2 durability;
        public int durabilityCost;
        public bool targeted;
        public bool aimed;
        public bool melee;
        public bool canHitWeakPoints;
        public bool infiniteDurability;


        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public List<Vector3> damage;
        
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
        public List<Vector3> attackTimings;
    }
}