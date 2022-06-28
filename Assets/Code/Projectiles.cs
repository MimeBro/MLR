using System;
using UnityEngine;

public class Projectiles : Attacks
{
    public float projectileSpeed;

    private new void Start()
    {
        Destroy(gameObject,5);
    }

    void Update()
    {
        transform.Translate(Vector3.right * (projectileSpeed * Time.deltaTime));
    }
    
}
