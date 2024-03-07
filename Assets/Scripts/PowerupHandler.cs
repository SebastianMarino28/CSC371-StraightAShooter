using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public bool isShield;
    public bool isClear;
    private int powerupsCollected;    

    void Awake() {
        powerupsCollected = 1;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && isShield) {
            other.gameObject.GetComponent<PlayerController>().UnlockShield();
            Destroy(gameObject);
            powerupsCollected++;
        }
    }

    public void doDeskFloorChange() {

    }
}
