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

    private Vector3 destination;

    public override void Start()
    {
        base.Start();
        var xOffset = Random.Range(-0.20f, 0.20f);

        var position = target.position;
        destination = new Vector3(position.x + xOffset, position.y);

        var transform1 = transform;
        transform1.right = destination - transform1.position;
    }

    private void Update()
    {
        if (Math.Abs(transform.position.y - destination.y) > 0.5f)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
        if (other.TryGetComponent(out Panel panel))
        {
            var component = GetComponent<Collider2D>();
            //component.enabled = false;
        }
    }
}
