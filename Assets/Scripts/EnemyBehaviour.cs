using System.Collections;
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
    public float speed;
    public float rotationSpeed;
    private bool idling = true;
    private PlayerController playerScript;

    private Vector3 lookDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerScript = target.GetComponent<PlayerController>();
        StartCoroutine(Idle());
    }

    void FixedUpdate()
    {
        if (healthAmount <= 0)
        {
            GameManager.instance.enemiesDestroyed += 1;
            Destroy(gameObject);
        }

        lookDirection = (target.transform.position - transform.position).normalized;

        lookDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        // try to shoot
        if (!idling)
        {
            if (weaponHandler != null)
            {
                weaponHandler.FireWeapon(projectileType);
            }
            if (speed > 0f)
            {
                Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
                rb.MovePosition(movement);
            }
            
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

    IEnumerator Idle()
    {
        idling = true;
        yield return new WaitForSeconds(0.5f);
        idling = false;
    }
}
