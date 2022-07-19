using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleProjectile : Attacks
{
    [Title("Simple Projectile")]
    public AttackDirection attackDirection;
    public float projectileSpeed;
    public Transform shootPoint;
    private new void Start()
    {
        var yOffset = Random.Range(0f, 1f);

        var position = shootPoint.position;
        transform.position = new Vector3(position.x, position.y + yOffset);
    }

    void Update()
    {
        if (attackDirection == AttackDirection.Forward)
        {
            transform.Translate(Vector3.right * (projectileSpeed * Time.deltaTime));
        }
        else if (attackDirection == AttackDirection.Backward)
        {
            transform.Translate(-Vector3.right * (projectileSpeed * Time.deltaTime));
        }
    }
    
}
