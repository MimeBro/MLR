using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuidedProjectile : Attacks
{
    [Title("Guided Projectile")] 
    public Transform target;
    public float speed;
    
    public override void Start()
    {
        transform.right = target.position - transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }
}
