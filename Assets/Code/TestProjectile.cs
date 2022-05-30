using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float projectileSpeed;
    void Update()
    {
        transform.Translate(Vector3.right * (projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col)
        {
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject,3);
        
    }
}
