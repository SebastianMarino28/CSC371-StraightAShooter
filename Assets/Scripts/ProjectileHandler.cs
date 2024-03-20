using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private SFXManager sfxManager;

    private void Start()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        if (gameObject.layer == 6 && gameObject.CompareTag("Projectile"))
        {
            GameManager.instance.projectilesFired++;
        }
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if (gameObject.layer == 6 && gameObject.CompareTag("Projectile"))
            {
                GameManager.instance.projectilesOnTarget++;
            }
        }

        if (gameObject.CompareTag("Projectile") && other.gameObject.CompareTag("Enemy"))
        {
            sfxManager.playBulletHit();
            GameManager.instance.projectilesOnTarget++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if (gameObject.layer == 6 && gameObject.CompareTag("Projectile"))
            {
                GameManager.instance.projectilesOnTarget++;
            }
        }

        if (gameObject.CompareTag("Projectile") && other.gameObject.CompareTag("Enemy"))
        {
            sfxManager.playBulletHit();
            GameManager.instance.projectilesOnTarget++;
        }
    }
}
