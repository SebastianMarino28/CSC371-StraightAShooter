using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private SFXManager sfxManager;

    private void Start()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (gameObject.CompareTag("Projectile") && other.gameObject.CompareTag("Enemy"))
        {
            sfxManager.playBulletHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (gameObject.CompareTag("Projectile") && other.gameObject.CompareTag("Enemy"))
        {
            sfxManager.playBulletHit();
        }
    }
}
