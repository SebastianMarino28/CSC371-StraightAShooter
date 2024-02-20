using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    private RoomTemplates templates;
    public GameObject room; // this will be an array of room templates later

    private bool[] configuration; // array of size 4 to store which doors are open (0 = top, 1 = bottom, 2 = left, 3 = right)
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false) {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Default Room");
            RoomHandler newRoom = Instantiate(prefab, transform.position, transform.rotation).GetComponent<RoomHandler>();
            rand = Random.Range(0, 7);
            if (openingDirection == 1) {
                // need to spawn a room with a bottom door
                //rand = Random.Range(0, templates.bottomRooms.Length);
                //Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                switch (rand)
                {
                    case 0: // bottom
                        configuration = new bool[]{false, true, false, false};
                        break;
                    case 1: // bottom, top
                        configuration = new bool[]{true, true, false, false};
                        break;
                    case 2: // bottom, right
                        configuration = new bool[]{false, true, false, true};
                        break;
                    case 3: // bottom, left
                        configuration = new bool[]{false, true, true, false};
                        break;
                    case 4: // bottom, right, left
                        configuration = new bool[]{false, true, true, true};
                        break;
                    case 5: // bottom, top, left
                        configuration = new bool[]{true, true, true, false};
                        break;
                    case 6: // bottom, top, right
                        configuration = new bool[]{true, true, false, true};
                        break;
                    default:
                        configuration = new bool[]{false, true, false, false};
                        break;     
                }
            }
            else if (openingDirection == 2) {
                // need to spawn a room with a top door
                //rand = Random.Range(0, templates.topRooms.Length);
                //Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                switch (rand)
                {
                case 0: // top
                    configuration = new bool[]{true, false, false, false};
                    break;
                case 1: // top, bottom
                    configuration = new bool[]{true, true, false, false};
                    break;
                case 2: // top, right
                    configuration = new bool[]{true, false, false, true};
                    break;
                case 3: // top, left
                    configuration = new bool[]{true, false, true, false};
                    break;
                case 4: // top, right, left
                    configuration = new bool[]{true, false, true, true};
                    break;
                case 5: // top, bottom, left
                    configuration = new bool[]{true, true, true, false};
                    break;
                case 6: // top, bottom, right
                    configuration = new bool[]{true, true, false, true};
                    break;
                default:
                    configuration = new bool[]{true, false, false, false};
                    break;     
                }
            }
            else if (openingDirection == 3) {
                // need to spawn a room with a left door
                //rand = Random.Range(0, templates.leftRooms.Length);
                //Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                switch (rand)
                {
                case 0: // left
                    configuration = new bool[]{false, false, true, false};
                    break;
                case 1: // left, bottom
                    configuration = new bool[]{false, true, true, false};
                    break;
                case 2: // left, right
                    configuration = new bool[]{false, false, true, true};
                    break;
                case 3: // left, top
                    configuration = new bool[]{true, false, true, false};
                    break;
                case 4: // left, right, top
                    configuration = new bool[]{true, false, true, true};
                    break;
                case 5: // left, bottom, top
                    configuration = new bool[]{true, true, true, false};
                    break;
                case 6: // left, bottom, right
                    configuration = new bool[]{false, true, true, true};
                    break;
                default:
                    configuration = new bool[]{false, false, true, false};
                    break;     
                }
            }
            else if (openingDirection == 4) {
                // need to spawn a room with a right door
                //rand = Random.Range(0, templates.rightRooms.Length);
                //Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                switch (rand)
                {
                case 0: // right
                    configuration = new bool[]{false, false, false, true};
                    break;
                case 1: // right, bottom
                    configuration = new bool[]{false, true, false, true};
                    break;
                case 2: // right, left
                    configuration = new bool[]{false, false, true, true};
                    break;
                case 3: // right, top
                    configuration = new bool[]{true, false, false, true};
                    break;
                case 4: // right, left, top
                    configuration = new bool[]{true, false, true, true};
                    break;
                case 5: // right, bottom, top
                    configuration = new bool[]{true, true, false, true};
                    break;
                case 6: // right, bottom, left
                    configuration = new bool[]{false, true, true, true};
                    break;
                default:
                    configuration = new bool[]{false, false, false, true};
                    break;     
                }
            }

            newRoom.ConfigureRoom(configuration);

            spawned = true;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("RoomSpawnPoint") && other.GetComponent<RoomSpawner>() != null) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // spawn walls blocking off any openings
                //Instantiate(templates.walls, transform.position, templates.walls.transform.rotation);
                GameObject prefab = (GameObject)Resources.Load("Prefabs/All Blocked Doors");
                Instantiate(prefab, transform.position, prefab.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
