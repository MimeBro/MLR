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
       
        public enum WeaponRarity {Common, Rare, Epic, Legendary, Mythic}
        
        [Title("Stats")] 
        public WeaponType weaponType;
        public WeaponRarity weaponRarity;
        
        [MinMaxSlider(0,100, true)]
        public Vector2 durability;
        
        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public Vector3 damage;

        [Title("Description")] 
        public string weaponName;
        [Title("Weapon Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string weaponDescription;
        
        [Title("Normal Attack Values")]
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
        public Vector3 attackTimings;

        public int attackDuraCost;
        
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

                if (timer >= attackTimings.z)
                {
                    Debug.Log("Attack missed");
                    _attackStarted = false;
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