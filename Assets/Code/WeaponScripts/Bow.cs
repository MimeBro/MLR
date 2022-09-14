using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.WeaponScripts
{
    public class Bow : Weapon
    {
        private RectTransform _aimReticle;
        public float reticleSpeed;
        private bool _aiming;
        public void OnEnable()
        {
            _aimReticle = BattleManager.Instance.aimReticle;
            
        }

        public override void Update()
        {
            if (_aiming)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                var vertical = Input.GetAxisRaw("Vertical");

                var movementDirection = new Vector2(horizontal, vertical);
                
                BattleManager.Instance.aimReticle.Translate(movementDirection * (reticleSpeed * Time.deltaTime));
            }
            base.Update();
        }

        public override void CastAttack()
        {
            base.CastAttack();
            BattleManager.Instance.aimReticle.gameObject.SetActive(true);
            _aimReticle.anchoredPosition = Vector3.zero;
            _aiming = true;
        }

        public override void EndAttack()
        {
            base.EndAttack();
            _aiming = false;
            BattleManager.Instance.aimReticle.gameObject.SetActive(false);
        }

        public override void Attack(float timing)
        {
            _aiming = false;
            BattleManager.Instance.aimReticle.gameObject.SetActive(false);
            base.Attack(timing);
        }
    }
}