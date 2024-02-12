using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject firePoint;
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
            Rigidbody rb = Instantiate(bullet, firePoint.transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.velocity = transform.forward * bulletSpeed;
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
