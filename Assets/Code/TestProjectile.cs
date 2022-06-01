using System;
using UnityEngine;

public class TestProjectile : Attacks
{
    public float projectileSpeed;

    private void Start()
    {
        Destroy(gameObject,5);
    }

    void Update()
    {
        transform.Translate(Vector3.right * (projectileSpeed * Time.deltaTime));
    }
    
}
