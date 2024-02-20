using UnityEngine;
using System.Collections.Generic;

public class RoomHandler : MonoBehaviour
{
    // 4 doors: 0 = top, 1 = bottom, 2 = left, 3 = right. If true, door in that location. Else, blocker.
    private bool[] doors;
    private bool hasEnemies = true;
    private List<GameObject> enemies = new List<GameObject>();

    [Header("Doors and Blockers")]
    public GameObject rightDoor;
    public GameObject leftDoor;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject rightBlocker;
    public GameObject leftBlocker;
    public GameObject topBlocker;
    public GameObject bottomBlocker;


    public void configureRoom(bool[] doorConfiguration)
    {
        doors = doorConfiguration;

        topDoor.SetActive(doors[0]);
        topBlocker.SetActive(!doors[0]);
        bottomDoor.SetActive(doors[1]);
        bottomBlocker.SetActive(!doors[1]);
        leftDoor.SetActive(doors[2]);
        leftBlocker.SetActive(!doors[2]);
        rightDoor.SetActive(doors[3]);
        rightBlocker.SetActive(!doors[3]);
    }


}
