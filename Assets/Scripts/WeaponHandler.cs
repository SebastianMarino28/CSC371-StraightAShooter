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

    [Header("Burst")]
    public GameObject burst;
    public AudioSource burstSound;
    public int burstSpeed;
    public float burstCooldown;
    public int numBurstProjectiles;
    private bool canFireBullet;
    private bool canFireBurst;
    public enum ProjectileType
    {
        bullet,
        burst
    }

    // Start is called before the first frame update
    void Start()
    {
        canFireBullet = true;
        canFireBurst = true;
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

        if (ptype == ProjectileType.burst && canFireBurst)
        {
            if (burstSound != null)
            {
                Instantiate(burstSound);
            }

            float[] directions = GetBurstDirections(numBurstProjectiles);

            Vector3 projectileDirection;
            Rigidbody rb;
            for (int i = 0; i < numBurstProjectiles; i++)
            {
                projectileDirection = new Vector3(0f, directions[i], 0f);
                
                GameObject projectile = Instantiate(burst, firePoint.transform.position, Quaternion.Euler(projectileDirection));
                rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = projectile.transform.forward * burstSpeed;
            }
            canFireBurst = false;
            StartCoroutine(ResetCooldown(ptype));

        }
    }

    public float[] GetBurstDirections(int numProjectiles)
    {   
        float angleStep = 360f / numProjectiles;
        float[] directions = new float[numProjectiles];

        for (int i = 0; i < numProjectiles; i++)
        {
            directions[i] = angleStep*i;
        }

        return directions;
    }

    IEnumerator ResetCooldown(ProjectileType ptype)
    {
        if (ptype == ProjectileType.bullet)
        {
            yield return new WaitForSeconds(bulletCooldown);
            canFireBullet = true;
        }

        if (ptype == ProjectileType.burst)
        {
            yield return new WaitForSeconds(burstCooldown);
            canFireBurst = true;
        }
    }
}
