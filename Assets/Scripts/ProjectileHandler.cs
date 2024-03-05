using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public SFXManager SFXManager;

    private void Start()
    {
        SFXManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            SFXManager.playBulletHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            SFXManager.playBulletHit();
        }
    }
}
