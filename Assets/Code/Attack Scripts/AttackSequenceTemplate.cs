using System.Collections;
using UnityEngine;

public class AttackSequenceTemplate : MonoBehaviour
{
    public float firerate;
    private float _nextfire;

    public GameObject projectile;
    public GameObject pillar;
    void Update()
    {
        if (Time.time >= _nextfire)
        {
            //StartPillarAttack();
            Instantiate(projectile, transform.position, Quaternion.identity);
            _nextfire = Time.time + firerate;
        }
    }

    public void StartPillarAttack()
    {
        StartCoroutine(PillarAttack());
    }

    private IEnumerator PillarAttack()
    {
        PanelsManager.Instance.PanelList[0].StartBlinking(1);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, PanelsManager.Instance.PanelList[0].transform.position,
            Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        var playerPanel = PanelsManager.Instance.PlayerPanel();
        playerPanel.StartBlinking(1);

        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, playerPanel.transform.position,
 Quaternion.identity);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
