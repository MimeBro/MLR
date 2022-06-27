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
      if (playerHasToLeave)
         PlayerController.Instance.playerMovement.PlayerLeaves();

      if (stopTimeToCast)
         StartCoroutine(StopTime());
   }

   public virtual void EnterScene()
   {
      
   }

   public virtual void CastAttack()
   {
      
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
