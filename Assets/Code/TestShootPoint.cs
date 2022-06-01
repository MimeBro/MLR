using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestShootPoint : MonoBehaviour
{
    public float firerate;
    private float _nextfire;

    public GameObject projectile;
    public GameObject pillar;
    void Update()
    {
        if (Time.time >= _nextfire)
        {
            //Instantiate(projectile, transform.position, quaternion.identity);
            _nextfire = Time.time + firerate;
        }
    }

    public void StartPillarAttack()
    {
        StartCoroutine(PillarAttack());
    }

    private IEnumerator PillarAttack()
    {
        GameManager.Instance.PanelList[0].StartBlinking(1);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, GameManager.Instance.PanelList[0].transform.position,
            quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlayerPanel().StartBlinking(1);
        var plyrpos = GameManager.Instance.PlayerPanel();
        
        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, plyrpos.transform.position,
 quaternion.identity);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(projectile, transform.position, quaternion.identity);
    }
}
