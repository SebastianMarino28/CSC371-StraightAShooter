using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public float maxHealth = 10f;
    public float healthAmount = 10f;
    public Image healthBar;
    public WeaponHandler weaponHandler;
    public WeaponHandler.ProjectileType projectileType;
    private GameObject target;
    public string targetTag;
    private Rigidbody rb;
    public int minDistance;

    private Vector3 lookDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }

        lookDirection = (target.transform.position - transform.position).normalized;

        transform.forward = lookDirection;

        // try to shoot
        if (Vector3.Distance(target.transform.position, transform.position) <=  minDistance)
            weaponHandler.FireWeapon(projectileType);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            TakeDamage(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / maxHealth;
    }
}
