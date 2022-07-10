using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuidedProjectile : Attacks
{
    public Transform target;
    public float speed;

    private Vector3 destination;
    private float xOffset;

    public override void Start()
    {
        base.Start();
        xOffset = Random.Range(-0.20f, 0.20f);

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
}
