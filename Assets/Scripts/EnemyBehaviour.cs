using Unity.VisualScripting;
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
    public float speed;
    private PlayerController playerScript;

    private Vector3 lookDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerScript = target.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }

        lookDirection = (target.transform.position - transform.position).normalized;
        transform.forward = lookDirection;

        // try to shoot
        if (Vector3.Distance(target.transform.position, transform.position) <= minDistance)
        {
            if (weaponHandler != null)
            {
                weaponHandler.FireWeapon(projectileType);
            }
            Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            rb.MovePosition(movement);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            TakeDamage(playerScript.damage);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / maxHealth;
    }
}
