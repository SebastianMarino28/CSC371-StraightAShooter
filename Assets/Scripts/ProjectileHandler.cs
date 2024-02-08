using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
