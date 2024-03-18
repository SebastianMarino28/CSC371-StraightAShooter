using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaserHandler : MonoBehaviour
{
    public GameObject sparks;
    public GameObject explosion;
    public GameObject hitP;
    private RaycastHit hit;
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
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second
    public float rotationTime;
    public float laserCooldown;
    private bool idling;
    Vector3 spin = new Vector3(0, 1, 0);

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        playerScript = target.GetComponent<PlayerController>();
        StartCoroutine(StartIdle(0.5f));
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            GameManager.instance.enemiesDestroyed += 1;
            GameObject effect = Instantiate(explosion);
            effect.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (canRotate && !idling)
        {
            isFiring = true;
            transform.Rotate(rotationSpeed * Time.deltaTime * spin);
            
        }

        if (!idling && isFiring && Physics.Raycast(firepoint.transform.position, transform.forward, out hit, float.MaxValue, layerMask))
        {
            laser.SetActive(true);
            midpoint = (firepoint.transform.position + hit.point) / 2;
            laser.transform.position = midpoint;
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, hit.distance/2, laser.transform.localScale.z);
        }
        else if (!isFiring || idling)
        {
            laser.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            GameObject effect = Instantiate(hitP);
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

    IEnumerator Rotate()
    {
        if (Random.Range(0, 2) == 0)
        {
            rotationSpeed = -rotationSpeed;
        }
        canRotate = true;
        //rotationTime = Random.Range(1f, 6f);
        yield return new WaitForSeconds(rotationTime);
        canRotate = false;
        isFiring = false;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {   
        yield return new WaitForSeconds(laserCooldown);
        StartCoroutine(Rotate());

    }

    IEnumerator StartIdle(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Rotate());
    }

    IEnumerator Idle(float time)
    {
        idling = true;
        yield return new WaitForSeconds(time);
        idling = false;
    }
}
