using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestShootPoint : MonoBehaviour
{
    public float _firerate;
    private float _nextfire;

    public GameObject projectile;
    void Update()
    {
        if (Time.time >= _nextfire)
        {
            Instantiate(projectile, transform.position, quaternion.identity);
            _nextfire = Time.time + _firerate;
        }
    }
}
