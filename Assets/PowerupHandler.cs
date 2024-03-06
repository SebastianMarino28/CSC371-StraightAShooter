using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public bool isShield;
    public bool isClear;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && isShield) {
            other.gameObject.GetComponent<PlayerController>().UnlockShield();
            Destroy(gameObject);
        }
    }
}
