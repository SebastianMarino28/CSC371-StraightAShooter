using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    private Rigidbody rb;
    public WeaponHandler weaponHandler;
    public WeaponHandler.ProjectileType projectileType;
    private BossPositions positions;
    private int positionIndex = 0;
    public GameObject laser;
    public LayerMask layerMask;
    public string targetTag;
    public GameObject firepoint;
    private GameObject target;
    private PlayerController playerScript;
    private Vector3 midpoint;
    public float maxHealth = 10f;
    public float healthAmount = 10f;
    public Image healthBar;
    private bool isFiring = false;
    private bool canRotate = false;
    public float moveSpeed;
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second
    public float rotationTime = 7f;
    public float laserCooldown;
    Vector3 spin = new Vector3(0, 1, 0);

    // state machine vars
    public Phase currentState;
    public enum Phase {
        laser,
        burst,
        perimeter
    }
    public float laserTime;
    public float perimeterTime;
    public float burstTime;

    void Start()
    {
        positions = GameObject.Find("Positions").GetComponent<BossPositions>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerScript = target.GetComponent<PlayerController>();
        StartCoroutine(Idle());
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            GameManager.instance.enemiesDestroyed += 1;
            Destroy(gameObject);
        }

        if (currentState == Phase.laser)
        {
            if (canRotate)
            {
                isFiring = true;
                transform.Rotate(rotationSpeed * Time.deltaTime * spin);
                
            }

            if (isFiring && Physics.Raycast(firepoint.transform.position, transform.forward, out hit, float.MaxValue, layerMask))
            {
                laser.SetActive(true);
                midpoint = (firepoint.transform.position + hit.point) / 2;
                laser.transform.position = midpoint;
                laser.transform.localScale = new Vector3(laser.transform.localScale.x, hit.distance/2, laser.transform.localScale.z);
            }
            else if (!isFiring)
            {
                laser.SetActive(false);
            }
        }
        else
        {
            canRotate = false;
            isFiring = false;
            laser.SetActive(false);
            if (currentState == Phase.perimeter)
            {
                Look();

                projectileType = WeaponHandler.ProjectileType.bullet;
                Shoot();
            }
            else if (currentState == Phase.burst)
            {
                projectileType = WeaponHandler.ProjectileType.burst;
                Shoot();
                transform.Rotate(rotationSpeed * Time.deltaTime * spin);
            }
        }
        
    }

    void FixedUpdate()
    {
        if (currentState == Phase.perimeter)
        {
            Move(positions.position[positionIndex % positions.position.Length].transform.position);
            if (Vector3.Distance(transform.position, positions.position[positionIndex % positions.position.Length].transform.position) < 5)
            {
                if (Random.Range(0,3) == 0)
                {
                    positionIndex--;
                }
                else
                {
                    positionIndex++;
                }
                if (positionIndex < 0)
                {
                    positionIndex = positions.position.Length - 1;
                }
                
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

    public void Move(Vector3 destination)
    {
        Vector3 movement = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movement);
    }

    public void Look()
    {
        transform.forward = (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position).normalized;
    }

    public void Shoot()
    {
        if (weaponHandler != null)
        {
            weaponHandler.FireWeapon(projectileType);
        }
    }

    IEnumerator Rotate()
    {
        if (currentState == Phase.laser)
        {
            rotationSpeed = -rotationSpeed;
            canRotate = true;
            yield return new WaitForSeconds(rotationTime);
            canRotate = false;
            isFiring = false;
            StartCoroutine(Delay());
        }
        
    }

    IEnumerator Delay()
    {   
        if (currentState == Phase.laser)
        {
            yield return new WaitForSeconds(laserCooldown);
            StartCoroutine(Rotate());
        }
        

    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(PhaseTimer());
    }

    IEnumerator PhaseTimer()
    {
        int rand = Random.Range(0,2);
        if (currentState == Phase.laser)
        {
            StartCoroutine(Rotate());
            yield return new WaitForSeconds(laserTime);
            if (rand == 0)
            {
                currentState = Phase.perimeter;
            }
            else if (rand == 1)
            {
                currentState = Phase.burst;
            }
            StartCoroutine(Idle());
        }
        else if (currentState == Phase.perimeter)
        {
            yield return new WaitForSeconds(perimeterTime);
            if (rand == 0)
            {
                currentState = Phase.laser;
            }
            else if (rand == 1)
            {
                currentState = Phase.burst;
            }
            StartCoroutine(Idle());
        }
        else if (currentState == Phase.burst)
        {
            yield return new WaitForSeconds(burstTime);
            if (rand == 0)
            {
                currentState = Phase.perimeter;
            }
            else if (rand == 1)
            {
                currentState = Phase.laser;
            }
            StartCoroutine(Idle());
        }
    }
}
