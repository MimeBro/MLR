using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchedProjectile : MonoBehaviour
{
    public float firerate;
    private float _nextfire;

    public SimpleProjectile simpleProjectile;
    public Panel targetPanel;

    public Ease easing;
    public float jumpPower = 5;
    public float duration;
    public OldUnit attacker;

    void Update()
    {
        if (Time.time >= _nextfire)
        {
             targetPanel = TeamManager.Instance.GetPlayerPanel();
            if(targetPanel == null) return;
            targetPanel.StartBlinking(1);
            var shot = Instantiate(simpleProjectile, transform.position, Quaternion.identity);
            shot.shootPoint = transform;
            shot.projectileSpeed = 0;
            shot.baseDamage = 5;
            shot.attacker = attacker;
            shot.transform.DOJump(targetPanel.transform.position, jumpPower, 1, duration).SetEase(easing);
            _nextfire = Time.time + firerate;
        }
    }
}
