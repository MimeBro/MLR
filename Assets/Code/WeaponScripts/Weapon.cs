<<<<<<< HEAD
using System.Collections.Generic;
=======
using System;
using System.Threading.Tasks;
using Code.CharacterScripts;
using Code.CommonScripts;
using MoreMountains.Tools;
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
    public class Weapon : ScriptableObject
    {
<<<<<<< HEAD
        [EnumToggleButtons]
        public WeaponType WeaponType;
        public WeaponRarity weaponRarity;
=======
       
        public enum WeaponRarity {Common, Rare, Epic, Legendary, Mythic}
        
        [Title("Stats")] 
        public WeaponType weaponType;
        public WeaponRarity weaponRarity;
        
        [MinMaxSlider(0,100, true)]
        public Vector2 durability;
        
        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public Vector3 damage;

        [Title("Description")] 
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
        public string weaponName;
        [Title("Weapon Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string weaponDescription;
<<<<<<< HEAD

        public Vector2 durability;
        public int durabilityCost;
        public bool targeted;
        public bool aimed;
        public bool melee;
        public bool canHitWeakPoints;
        public bool infiniteDurability;


        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public List<Vector3> damage;
=======
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
        
        [Title("Normal Attack Values")]
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
<<<<<<< HEAD
        public List<Vector3> attackTimings;
=======
        public Vector3 attackTimings;

        public int attackDuraCost;
        
        public Player player;
        private bool _attackStarted, _attackEnded;
        private float timer = 0.0f;

        private void OnEnable()
        {
            //_player = BattleManager.Instance.GetPlayer();
        }

        public virtual void Update()
        {
            AttackTimer();
        }

        private void AttackTimer()
        {
            if (_attackStarted)
            {
                timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack(timer);
                }

                if (timer >= attackTimings.z)
                {
                    Debug.Log("Attack missed");
                    _attackStarted = false;
                    player.EndAttack();
                }
            }
        }

        public virtual async void CastAttack()
        {
            timer = 0.0f;
            durability.x -= attackDuraCost;
            _attackStarted = true;
        }

        public virtual void Attack(float timing)
        {
            _attackStarted = false;

            if (timing < attackTimings.x)
            {
                Debug.Log("Ok Hit");
                player.EndAttack();
            }
            
            else if (timing >= attackTimings.x && timing < attackTimings.y)
            {
                Debug.Log("Good Hit");
                player.EndAttack();
            }
            
            else if (timing >= attackTimings.y)
            {
                Debug.Log("Perfect Hit");
                player.EndAttack();
            }
        }

        public virtual async void SpecialAttack()
        {
            
        }
        
        
>>>>>>> parent of 5dace37 (Enemy Selection During Battle)
    }
}