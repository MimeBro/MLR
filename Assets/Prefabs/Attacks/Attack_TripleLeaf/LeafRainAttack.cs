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

    private void LateUpdate()
    {
        currentPanelIndex = GameManager.Instance.PanelList.IndexOf(PlayerController.Instance.unit.currentPanel);

    }

    public void StartLeafAttack()
    {
        StartCoroutine(LeafRain());
    }

    public IEnumerator LeafRain()
    {
        var shootPosition = PlayerController.Instance.shootPoint.position;
        var lastPanel = GameManager.Instance.PanelList.Count - 1;
        List<Panel> PanelList = GameManager.Instance.PanelList;
        var increasing = 1;

        yield return new WaitForSeconds(waitTime);

        if (currentPanelIndex == lastPanel) yield break;

        for (int i = 0; i < 3; i++)
        {
            if (currentPanelIndex + increasing < lastPanel)
            {
                var clone = Instantiate(leafAttack, shootPosition, Quaternion.identity);
                clone.target = PanelList[currentPanelIndex + increasing].transform;
                clone.speed = projectilesSpeed;
                increasing++;
            }
            else
            {
                var clone = Instantiate(leafAttack, shootPosition, Quaternion.identity);
                clone.target = GameManager.Instance.PanelList[lastPanel].transform;
                clone.speed = projectilesSpeed;
            }
        }
        
    }
}
