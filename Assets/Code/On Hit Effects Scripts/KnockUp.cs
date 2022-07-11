using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Add this component to an attack in order to make the receiving unit get knocked up.
/// </summary>
public class KnockUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Unit unit))
        {
            unit.CastHitEffect(HitEffects.KNOCKUP);
        }
    }
}
