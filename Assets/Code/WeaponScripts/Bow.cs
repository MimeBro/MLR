using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.WeaponScripts
{
    public class Bow : Weapon
    {
        private RectTransform _aimReticle;
        public float aimingSpeed = 10f;
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
                var aimingCam = BattleManager.Instance.aimingCam.transform;
                
                aimingCam.Translate(movementDirection * (aimingSpeed * Time.deltaTime));
                
                aimingCam.localPosition = new Vector3(
                    Mathf.Clamp(aimingCam.localPosition.x, -9f, 35f), 
                    Mathf.Clamp(aimingCam.localPosition.y, 1f, 13f), 
                    aimingCam.localPosition.z);
            }
            base.Update();
        }

        public override void CastAttack()
        {
            base.CastAttack();
            BattleManager.Instance.aimingCam.transform.position = BattleManager.Instance.waitingCam.transform.position;
            BattleManager.Instance.aimReticle.gameObject.SetActive(true);
            BattleManager.Instance.CameraAimingPosition();
            _aimReticle.anchoredPosition = Vector3.zero;
            _aiming = true;
        }

        public override void EndAttack()
        {
            BattleManager.Instance.aimReticle.gameObject.SetActive(false);
            BattleManager.Instance.CameraWaitingPosition();
            base.EndAttack();
            _aiming = false;
        }

        public override void Attack(float timing)
        {
            _aiming = false;
            BattleManager.Instance.aimReticle.gameObject.SetActive(false);
            base.Attack(timing);
        }
    }
}