using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public bool isShield;
    public bool isClear;
    private int powerupsCollected;    
    private GameObject currentFloor;

    void Awake() {
        powerupsCollected = 1;
        currentFloor = GameObject.Find("RedFloor");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && isShield) {
            other.gameObject.GetComponent<PlayerController>().UnlockShield();
            Destroy(gameObject);
            powerupsCollected++;
            doDeskFloorChange();
        }
    }

    public void doDeskFloorChange() {
        GameObject dorm_object = GameObject.Find("DormRoomTeleporter");
        if(powerupsCollected == 2) {
            Destroy(currentFloor);
            GameObject orange_carpet = (GameObject)Resources.Load("Prefabs/DifficultyFloor/OrangeFloor");
            currentFloor = Instantiate(orange_carpet, dorm_object.transform);
        }
        if(powerupsCollected == 3) {
            GameObject yellow_carpet = (GameObject)Resources.Load("Prefabs/DifficultyFloor/YellowFloor");
            Instantiate(yellow_carpet, dorm_object.transform);
        }
        // change prefab instance of the floor under the desk, each color indicating difficulty of the boss
        //  1 = red floor
        //  2 = orange floor
        //  3 = yellow floor
    }
}
