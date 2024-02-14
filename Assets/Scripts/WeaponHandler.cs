using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject centerPoint;
    [Header("Bullet")]
    public GameObject bullet;
    public AudioSource bulletSound;
    public int bulletSpeed;
    public float bulletCooldown;
    private bool canFireBullet;

    public enum ProjectileType
    {
        bullet
    }

    // Start is called before the first frame update
    void Start()
    {
        canFireBullet = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireWeapon(ProjectileType ptype)
    {
        if (ptype == ProjectileType.bullet && canFireBullet)
        {
            if (bulletSound != null)
            {
                Instantiate(bulletSound);
            }

            // calculate direction
            Vector3 forceDirection = centerPoint.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(centerPoint.transform.position, centerPoint.transform.forward, out hit))
            {
                forceDirection = (hit.point - firePoint.transform.position).normalized;
            }

            Rigidbody rb = Instantiate(bullet, firePoint.transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.velocity = forceDirection * bulletSpeed;
            canFireBullet = false;
            StartCoroutine(ResetCooldown(ptype));
        }
    }

    IEnumerator ResetCooldown(ProjectileType ptype)
    {
        if (ptype == ProjectileType.bullet)
        {
            yield return new WaitForSeconds(bulletCooldown);
            canFireBullet = true;
        }
    }
}
