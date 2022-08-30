using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float shotsPerSecond;
    public int damagePerShot;
    private float _firerate;
    private float _nextfire;
    
    public Transform shootPoint;
    public GameObject projectile;
    private Animator _animator;
    private OldUnit _oldUnit;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _oldUnit = GetComponent<OldUnit>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Attack();
        }
    }

    public void Attack()
    {
        _firerate = 1 / shotsPerSecond;
        if (Time.time >= _nextfire)
        {
            if (PlayerController.Instance.commandBuffer.Any()) return;
            PlayerController.Instance.AddCommand(ProjectileBasicAttack,0);
            _nextfire = Time.time + _firerate;
        }
    }

    public void ProjectileBasicAttack()
    {
        var bullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        var shots = bullet.GetComponentsInChildren<Attacks>();
        foreach (var shot in shots)
        {
            shot.side = _oldUnit.side;
            shot.baseDamage = damagePerShot;
        }
    }

    public void MeleeBasicAttack()
    {
        
    }
}
