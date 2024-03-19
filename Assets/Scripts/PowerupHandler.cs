using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    private UIButtonBehaviour ui_buttons;
    public bool isShield;
    public bool isEraser;
    private int powerupsCollected;    
    private GameObject currentFloor;

    void Awake() {
        ui_buttons = GameObject.Find("PauseScreen").GetComponent<UIButtonBehaviour>();
        powerupsCollected = 1;
        currentFloor = GameObject.Find("RedFloor");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && isShield) {
            other.gameObject.GetComponent<PlayerController>().UnlockShield();
            Destroy(gameObject);
            powerupsCollected++;
            doDeskFloorChange();
            ui_buttons.BackpackClick();
        }
        if (other.gameObject.CompareTag("Player") && isEraser) {
            other.gameObject.GetComponent<PlayerController>().UnlockEraser();
            Destroy(gameObject);
            powerupsCollected++;
            doDeskFloorChange();
            ui_buttons.BackpackClick();
        }
    }

        /* changes prefab instance of the floor under the desk, each color indicating difficulty of the boss
            1 = red floor
            2 = orange floor
            3 = yellow floor */
    public void doDeskFloorChange() {
        GameObject dorm_object = GameObject.Find("DormRoomTeleporter");
        if(powerupsCollected == 2) {
            Destroy(currentFloor);
            GameObject orange_carpet = (GameObject)Resources.Load("Prefabs/DifficultyFloor/OrangeFloor");
            currentFloor = Instantiate(orange_carpet, dorm_object.transform);
        }
        if(powerupsCollected == 3) {
            Destroy(currentFloor);
            GameObject yellow_carpet = (GameObject)Resources.Load("Prefabs/DifficultyFloor/YellowFloor");
            currentFloor = Instantiate(yellow_carpet, dorm_object.transform);
        }
    }
}
