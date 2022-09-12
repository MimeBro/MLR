using System;
using System.Threading.Tasks;
using Code.CharacterScripts;
using Code.CommonScripts;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponType{Sword, GreatSword, Axe, Hammer, Spear, Bow, Staff, Book}
        public enum WeaponRarity {Common, Rare, Epic, Legendary, Mythic}
        
        [Title("Stats")] 
        public WeaponType weaponType;
        public WeaponRarity weaponRarity;
        
        [MinMaxSlider(0,100, true)]
        public Vector2 durability;
   
        public Vector3 damage;

        [Title("Description")] 
        public string weaponName;
        [Title("Weapon Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string weaponDescription;
        
        public string specialAttack;
        [Title("Special Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string specialAttackDescription;
        
        [Title("Normal Attack Values")]
        public float attackDuration;
        public Vector2 attackTimings;
        
        private Player _player;
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
                Debug.Log(timer);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack(timer);
                }

                if (timer >= attackDuration)
                {
                    Debug.Log("Attack missed");
                    _attackStarted = false;
                }
            }
        }

        public virtual async void CastAttack()
        {
            timer = 0.0f;
            _attackStarted = true;
        }

        public virtual void Attack(float timing)
        {
            _attackStarted = false;

            if (timing < attackTimings.x)
            {
                Debug.Log("Ok Hit");
            }
            
            else if (timing >= attackTimings.x && timing < attackTimings.y)
            {
                Debug.Log("Good Hit");
            }
            
            else if (timing >= attackTimings.y)
            {
                Debug.Log("Perfect Hit");
            }
        }

        public virtual async void SpecialAttack()
        {
            
        }
        
        
    }
}