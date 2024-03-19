using UnityEngine;
using System;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 0 --> need top door
    // 1 --> need bottom door
    // 2 --> need left door
    // 3 --> need right door
    private RoomTemplates templates;
    public GameObject room; // this will be an array of room templates later
    private int roomCutoff = 25;

    private bool[] configuration; // array of size 4 to store which doors are open (0 = top, 1 = bottom, 2 = left, 3 = right)
    private bool spawned = false;
    int roomTypeCount = 3;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false) {
            int randRoom = UnityEngine.Random.Range(1, roomTypeCount + 1);
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Room" + randRoom);
            RoomHandler newRoom = Instantiate(prefab, transform.position, transform.rotation).GetComponent<RoomHandler>();
            configuration = new bool[] { false, false, false, false };
            configuration[openingDirection] = true;

            if (GameManager.instance.roomsTotal < roomCutoff)
            {
                // guarantee rooms will have at least 2 doors until cutoff
                List<int> possibleDoors = new() {0, 1, 2, 3};
                possibleDoors.Remove(openingDirection);
                int guaranteedDoor = possibleDoors[UnityEngine.Random.Range(0, 3)];
                configuration[guaranteedDoor] = true;


                int doors = 1;
                for (int i = 0; i < 4; i++)
                {
                    if (!configuration[i] && doors>0)
                    {
                        bool putRoom = (UnityEngine.Random.Range(0, 2) == 1);
                        if (putRoom)
                        {
                            doors--;
                            configuration[i] = true;
                        }
                    }
                }
            }

            int x = (int)Math.Floor(transform.position.x / 20);
            int y = (int)Math.Floor(transform.position.z / 20);
            newRoom.ConfigureRoom(x, y, configuration);

            spawned = true;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("RoomSpawnPoint") && other.GetComponent<RoomSpawner>() != null) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // spawn blocked doors blocking off any openings
                GameObject prefab = (GameObject)Resources.Load("Prefabs/All Blocked Doors");
                Instantiate(prefab, transform.position, prefab.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
