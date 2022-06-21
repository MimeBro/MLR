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
        GameManager.Instance.PanelList[0].StartBlinking(1);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, GameManager.Instance.PanelList[0].transform.position,
            Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => GameManager.Instance.PlayerPanel() != null);
        GameManager.Instance.PlayerPanel().StartBlinking(1);
        var plyrpos = GameManager.Instance.PlayerPanel();

        yield return new WaitForSeconds(1);
        
        Instantiate(pillar, plyrpos.transform.position,
 Quaternion.identity);
        
        yield return new WaitForSeconds(1);
        
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
