using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public GameObject SFXManager;


    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioSource.volume =
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") && collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
        }
    }
}
