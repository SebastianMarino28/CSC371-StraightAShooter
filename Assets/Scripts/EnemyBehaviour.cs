using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject sparks;
    public GameObject explosion;
    public GameObject hit;
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
    public float idleTime = 0.5f;
    private PlayerController playerScript;

    private Vector3 lookDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerScript = target.GetComponent<PlayerController>();
        StartCoroutine(Idle(idleTime));
    }

    void FixedUpdate()
    {
        if (healthAmount <= 0)
        {
            GameManager.instance.enemiesDestroyed += 1;
            GameObject effect = Instantiate(explosion);
            effect.transform.position = transform.position;
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
            GameObject effect = Instantiate(hit);
            effect.transform.position = transform.position;
            TakeDamage(playerScript.damage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Eraser"))
        {
            StartCoroutine(Idle(2));
            GameObject effect = Instantiate(sparks);
            effect.transform.position = transform.position;
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / maxHealth;
    }

    IEnumerator Idle(float time)
    {
        idling = true;
        yield return new WaitForSeconds(time);
        idling = false;
    }
}
