using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuidedProjectile : MonoBehaviour
{
    public Transform target;
    public Vector3 destination;
    public float speed;

    private float xOffset;

    private void Start()
    {
        xOffset = Random.Range(-0.20f, 0.20f);

        var position = target.position;
        destination = new Vector3(position.x + xOffset, position.y);

        var transform1 = transform;
        transform1.right = destination - transform1.position;
    }

    private void Update()
    {
        if (Math.Abs(transform.position.y - destination.y) > 0.3f)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }
}
