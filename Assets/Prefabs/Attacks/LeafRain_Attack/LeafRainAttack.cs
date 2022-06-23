using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafRainAttack : Attacks
{
    public int currentPanelIndex;
    public float projectilesSpeed;

    public float waitTime;
    public GuidedProjectile leafAttack;

    public float jumpDuration;
    public float moveDuration;

    public override void Start()
    {
        currentPanelIndex = GameManager.Instance.PanelList.IndexOf(PlayerController.Instance.unit.currentPanel);
        PlayerController.Instance.AddCommand(StartLeafAttack,jumpDuration);
        Debug.Log(damage);
    }
    
    public void StartLeafAttack()
    {
        StartCoroutine(LeafRain());
    }

    public IEnumerator LeafRain()
    {
        PlayerController.Instance.animator.SetTrigger("JumpAttack");
        PlayerController.Instance.playerMovement.KnockBack(moveDuration);

        yield return new WaitForSeconds(waitTime);

        var shootPosition = PlayerController.Instance.shootPoint.position;
        
        var lastPanel = GameManager.Instance.PanelList.Count - 1;
        
        List<Panel> PanelList = GameManager.Instance.PanelList;
        
        var increasing = 1;

        if (currentPanelIndex == lastPanel) yield break;

        for (int i = 0; i < 3; i++)
        {
            if (currentPanelIndex + increasing < lastPanel)
            {
                var clone = Instantiate(leafAttack, shootPosition, Quaternion.identity);
                clone.target = PanelList[currentPanelIndex + increasing].transform;
                clone.speed = projectilesSpeed;
                clone.SetSide(side);
                clone.SetDamage(damage);
                clone.pierceThrough = pierceThrough;
                increasing++;
            }
            else
            {
                var clone = Instantiate(leafAttack, shootPosition, Quaternion.identity);
                clone.target = GameManager.Instance.PanelList[lastPanel].transform;
                clone.speed = projectilesSpeed;
                clone.SetSide(side);
                clone.SetDamage(damage);
                clone.damage = damage;
                clone.pierceThrough = pierceThrough;
            }
        }
        
        Destroy(gameObject);
    }
}
