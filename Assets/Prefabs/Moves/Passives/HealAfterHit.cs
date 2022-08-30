using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAfterHit : PassiveMove
{
   public int healAmount;

   public void Heal()
   {
       PlayerController.Instance.oldUnit.Heal(healAmount);
       callerButton?.ConditionsMet();
       Destroy(gameObject);
   }
}
