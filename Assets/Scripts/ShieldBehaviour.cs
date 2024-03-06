using System.Collections;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;
    public float shieldTime;
    public float baseProjectileDamage;

    void Start()
    {
        StartCoroutine(ActiveShield());
    }


    void Update()
    {
        
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
        yield return new WaitForSeconds(shieldTime);
        Destroy(gameObject);
    }
}
