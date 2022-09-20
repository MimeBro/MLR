using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.CharacterScripts;
using Code.CommonScripts;
using Code.MonsterScripts;
using DG.Tweening;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.WeaponScripts
{
    public class WeaponAttack : MonoBehaviour
    {
        public Player player;
        [MinMaxSlider(0,100, true)]
        private Vector2 _durability;
        
        [Tooltip("If the attack needs a target to be casted on")]
        protected bool _targeted;
        private Enemy _target;
        
        [InfoBox("X is the OK hit damage, Y the Good hit dmg and Z the Perfect Dmg")]
        [SerializeField]protected List<Vector3> _damage;
        [InfoBox("X is the Good hit timing, Y the Perfect hit timing and Z the attack duration")]
        [SerializeField]protected List<Vector3> _attackTimings;
        protected int _currentAttack = 0;
        protected int _durabilityCost;
        public bool infiniteDurability;

        protected bool _attackStarted;
        protected float timer = 0.0f;
        
        private float _aimingSpeed = 10f;
        private RectTransform _crosshair;
        private bool _aiming;

        [Title("Normal Attack")] 
        public Weapon weapon;
        [Title("Special Attack")] 
        public SpecialAttack specialAttack;


        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        public virtual void Update()
        {
            AttackTimer();
            Aiming();
        }


        #region Attacking
        public virtual void AttackTimer()
        {
            if (_attackStarted)
            {
                timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Attack(timer);
                }

                if (timer >= _attackTimings[_currentAttack].z)
                {
                    EndAttack();
                }
            }
        }



        public virtual void CastAttack()
        {
            Debug.Log("Attack Casted");
            timer = 0.0f;
            _currentAttack = 0;
            _damage = weapon.damage;
            _attackTimings = weapon.attackTimings;
            _durabilityCost = weapon.durabilityCost;
            _targeted = weapon.targeted;
            if(!infiniteDurability) weapon.durability.x -= _durabilityCost;
            _attackStarted = true;
            if(weapon.aimed) AimAttack();
        }
        
        public virtual void CastAttack(Enemy target)
        {
            _target = target;
            Debug.Log("Attack Casted");
            timer = 0.0f;
            _currentAttack = 0;
            _damage = weapon.damage;
            _attackTimings = weapon.attackTimings;
            _durabilityCost = weapon.durabilityCost;
            _targeted = weapon.targeted;
            if(!infiniteDurability) weapon.durability.x -= _durabilityCost;
            if (weapon.melee)
            {
                MeleeAttack(target);
            }
            else
            {
                _attackStarted = true;
            }
        }

        public virtual void CastSpecialAttack()
        {
            if(specialAttack == null) return;
            Debug.Log("Special Attack Casted");
            timer = 0.0f;
            _currentAttack = 0;
            _damage = specialAttack.damage;
            _attackTimings = specialAttack.attackTimings;
            _durabilityCost = specialAttack.durabilityCost;
            _targeted = specialAttack.targeted;
            if(!infiniteDurability) weapon.durability.x -= _durabilityCost;
            _attackStarted = true;
            if(specialAttack.aimed) AimAttack();
        }


        public virtual void CastSpecialAttack(Enemy target)
        {
            _target = target;
            timer = 0.0f;
            _currentAttack = 0;
            _damage = specialAttack.damage;
            _attackTimings = specialAttack.attackTimings;
            _durabilityCost = specialAttack.durabilityCost;
            _targeted = specialAttack.targeted;
            if(!infiniteDurability) weapon.durability.x -= _durabilityCost;
            if (specialAttack.melee)
            {
                MeleeAttack(target);
            }
            else
            {
                _attackStarted = true;
            }
        }

        public void AimAttack()
        {
            if(_crosshair == null) _crosshair = BattleManager.Instance.crosshair;
            BattleManager.Instance.aimingCam.transform.position = BattleManager.Instance.waitingCam.transform.position;
            BattleManager.Instance.crosshair.gameObject.SetActive(true);
            BattleManager.Instance.CameraAimingPosition();
            _crosshair.anchoredPosition = Vector3.zero;
            _aiming = true;
        }
        
        //For attacks that use the Crosshair.
        public void Aiming()
        {
            if (_aiming)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                var vertical = Input.GetAxisRaw("Vertical");

                var movementDirection = new Vector2(horizontal, vertical);
                var aimingCam = BattleManager.Instance.aimingCam.transform;
                
                aimingCam.Translate(movementDirection * (_aimingSpeed * Time.deltaTime));
                
                aimingCam.localPosition = new Vector3(
                    Mathf.Clamp(aimingCam.localPosition.x, -9f, 35f), 
                    Mathf.Clamp(aimingCam.localPosition.y, 1f, 13f), 
                    aimingCam.localPosition.z);
            }
        }

        public async void MeleeAttack(Enemy target)
        {
            var position = target.transform.position;
            var destination = new Vector3(position.x - 5, player.transform.position.y, position.z);
            await BattleManager.Instance.Wait(0.5f);
            player.transform.DOJump(destination, 1,1,0.4f).SetEase(Ease.Linear).OnComplete(()=>
                _attackStarted = true);
        }


        public virtual void Attack(float timing)
        {
            _attackStarted = false;
            
            if (timing < _attackTimings[_currentAttack].x)
            {
                Debug.Log("Ok Hit, Damage: " + _damage[_currentAttack].x );
                NextAttack();
            }
            
            else if (timing >= _attackTimings[_currentAttack].x && timing < _attackTimings[_currentAttack].y)
            {
                Debug.Log("Good Hit: " + _damage[_currentAttack].y);
                NextAttack();
            }
            
            else if (timing >= _attackTimings[_currentAttack].y)
            {
                Debug.Log("Perfect Hit: " + _damage[_currentAttack].z);
                NextAttack();
            }
        }

        public void NextAttack()
        {
            _currentAttack++;
            if (_attackTimings.Count > _currentAttack)
            {
                timer = 0.0f;
                _attackStarted = true;
            }
            else
            {
                EndAttack();
                _currentAttack = 0;
            }
        }

        public virtual void EndAttack()
        {
            if(_aiming)
            { 
                BattleManager.Instance.crosshair.gameObject.SetActive(false);
                BattleManager.Instance.CameraWaitingPosition();
                _aiming = false;
            }
            _attackStarted = false;
            player.EndAttack();
        }
        
        #endregion
  
    }
}