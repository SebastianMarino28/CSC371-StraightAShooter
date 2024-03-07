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

    public void doDeskFloorChange(int powerupsCollected) {
        // change prefab instance of the floor under the desk, each color indicating difficulty of the boss
        //  1 = red floor
        //  2 = orange floor
        //  3 = yellow floor
    }
}
