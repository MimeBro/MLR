using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Unit>())
        {
            Unit u;
            
            u = col.GetComponent<Unit>();
            
            if (u.uState == UnitState.DODGING)
            {
                Debug.Log("Dodged the Attack");
            }
            else
            {
                Debug.Log("Hit Player");
                Destroy(gameObject);
            }
        }
    }
}
