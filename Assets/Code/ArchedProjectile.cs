using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchedProjectile : MonoBehaviour
{
    public float firerate;
    private float _nextfire;

    public Projectiles projectile;
    public Panel targetPanel;

    public Ease easing;
    public float jumpPower = 5;
    public float duration;

    void Update()
    {
        if (Time.time >= _nextfire)
        {
            targetPanel = PanelsManager.Instance.PlayerPanel();
            if(targetPanel == null) return;
            targetPanel.StartBlinking(1);
            var shot = Instantiate(projectile, transform.position, Quaternion.identity);
            shot.projectileSpeed = 0;
            shot.damage = 5;
            shot.transform.DOJump(targetPanel.transform.position, jumpPower, 1, duration).SetEase(easing);
            _nextfire = Time.time + firerate;
        }
    }
}
