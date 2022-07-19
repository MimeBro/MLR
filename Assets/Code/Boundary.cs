using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Attacks attacks))
        {
            Destroy(attacks.gameObject);
        }
    }
}
