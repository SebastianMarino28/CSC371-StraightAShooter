using System.Collections;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public GameObject impact;
    private MeshRenderer mr;
    private Material shieldColor;
    private Color newColor;
    private Color initialColor;
    public Color finalColor;
    public float speed;
    private float colorCount = 0;
    public float curHealth;
    public float maxHealth;
    public float shieldTime;
    public float baseProjectileDamage;
    private bool active = false;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        shieldColor = GetComponent<Renderer>().material;
        initialColor = shieldColor.color;
        GameObject effect = Instantiate(impact);
        effect.transform.position = transform.position;
        StartCoroutine(ActiveShield());
    }


    void Update()
    {   
        if (active)
        {
            newColor = Color.Lerp(initialColor, finalColor, colorCount);
            shieldColor.color = newColor;
            colorCount += speed * Time.deltaTime;
        }    
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Projectile")) {
            Destroy(other.gameObject);
            TakeDamage(baseProjectileDamage);
        }
        if (other.gameObject.CompareTag("Laser")) {
            Destroy(gameObject);
           
        }
    }

    public void TakeDamage(float damage)
    {
        if(curHealth > 0) {
            curHealth -= damage;
        }
        if(curHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public IEnumerator ActiveShield()
    {
        yield return new WaitForSeconds(0.25f);
        mr.enabled = true;
        active = true;
        yield return new WaitForSeconds(shieldTime);
        Destroy(gameObject);
    }
}
