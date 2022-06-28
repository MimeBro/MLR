using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
   public bool stopTimeToCast;
   public bool playerHasToLeave;
   public float attackDuration;
   public float timeStopDuration;

   public AAttack moveToCast;

   public void StartAttack()
   {
        StartCoroutine(EnterScene());
   }

   public virtual IEnumerator EnterScene()
   {
        PlayerMovement playerMovement = PlayerController.Instance.playerMovement;
        var ypos = playerMovement.yposition;
        var cpanel = PlayerController.Instance.unit.currentPanel.transform.position;
        
        var destination = new Vector2(cpanel.x, ypos);

        playerMovement.PlayerLeaves();
        transform.DOMove(destination, 0.2f);
        yield return new WaitForSecondsRealtime(0.5f);
        CastAttack();
        yield return new WaitForSeconds(3);
        playerMovement.PlayerComesBack();
        Destroy(gameObject);
   }

   public virtual void CastAttack()
   {
        moveToCast.CastAttack();
   }

   public virtual void LeaveScene()
   {
      
   }

   public IEnumerator StopTime()
   {
      DOVirtual.Float(1, 0, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
      yield return new WaitForSecondsRealtime(timeStopDuration);
      DOVirtual.Float(0, 1, 0.3f, t => { Time.timeScale = t; }).SetUpdate(true);
   }
}
