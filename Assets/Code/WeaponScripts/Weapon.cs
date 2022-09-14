using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.CharacterScripts;
using Code.CommonScripts;
using Code.MonsterScripts;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponRarity {Common, Rare, Epic, Legendary, Mythic}

        public Player player;

        [Title("Stats")] 
        public WeaponType weaponType;

        public WeaponRarity weaponRarity;


        [MinMaxSlider(0,100, true)]
        public Vector2 durability;


        [Title("Description")] 
        public string weaponName;

        [Title("Weapon Description: ", bold:false)]
        [HideLabel]
        [Multiline(5)]
        public string weaponDescription;

        [Title("Normal Attack Values")] 
        [Tooltip("If the attack needs a target to be casted on")]
        public bool targeted;
        private Enemy _target;
        
        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        public List<Vector3> damage;
        
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
        public List<Vector3> attackTimings;

        private int _currentAttack = 0;

        public int attackDuraCost;
        public bool infiniteDurability;

        private bool _attackStarted, _attackEnded;
        private float timer = 0.0f;
        
        public virtual void Update()
        {
            AttackTimer();
        }

        private void AttackTimer()
        {
            if (_attackStarted)
            {
                timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Attack(timer);
                }

                if (timer >= attackTimings[_currentAttack].z)
                {
                    Debug.Log("Attack missed");
                    _attackStarted = false;
                    player.EndAttack();
                }
            }
        }

        public virtual void CastAttack()
        {
            timer = 0.0f;
            _currentAttack = 0;
            if(!infiniteDurability) durability.x -= attackDuraCost;
            _attackStarted = true;
        }

        public virtual void CastAttack(Enemy target)
        {
            _target = target;
            timer = 0.0f;
            _currentAttack = 0;
            if(!infiniteDurability) durability.x -= attackDuraCost;
            _attackStarted = true;
        }

        public virtual void Attack(float timing)
        {
            _attackStarted = false;
            
            if (timing < attackTimings[_currentAttack].x)
            {
                Debug.Log("Ok Hit, Damage: " + damage[_currentAttack].x );
                NextAttack();
            }
            
            else if (timing >= attackTimings[_currentAttack].x && timing < attackTimings[_currentAttack].y)
            {
                Debug.Log("Good Hit: " + damage[_currentAttack].y);
                NextAttack();
            }
            
            else if (timing >= attackTimings[_currentAttack].y)
            {
                Debug.Log("Perfect Hit: " + damage[_currentAttack].z);
                NextAttack();
            }
        }

        public void NextAttack()
        {
            _currentAttack++;
            if (attackTimings.Count > _currentAttack)
            {
                timer = 0.0f;
                _attackStarted = true;
            }
            else
            {
                player.EndAttack();
                _currentAttack = 0;
            }
        }

        public virtual async void SpecialAttack()
        {
            
        }
        
        
    }
}