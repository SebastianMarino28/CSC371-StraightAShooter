using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomHandler : MonoBehaviour
{
    // 4 doors: 0 = top, 1 = bottom, 2 = left, 3 = right. If true, door in that location. Else, blocker.
    private bool[] doors;
    public List<GameObject> enemiesPoolEasy = new List<GameObject>();
    public List<GameObject> enemiesPoolMedium = new List<GameObject>();
    public List<GameObject> enemiesPoolHard = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private bool entered = false;
    private bool enemiesSpawned = false;
    private bool upgradeSpawned = false;
    public GameObject upgrade;
    public int mapX;
    public int mapY;
    private SFXManager sfxManager;
    

    [Header("Doors and Blockers")]
    public GameObject rightDoor;
    public GameObject leftDoor;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject rightBlocker;
    public GameObject leftBlocker;
    public GameObject topBlocker;
    public GameObject bottomBlocker;
    public GameObject rightSpawnPoint;
    public GameObject leftSpawnPoint;
    public GameObject topSpawnPoint;
    public GameObject bottomSpawnPoint;


    private void Start()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    public void ConfigureRoom(int x, int y, bool[] doorConfiguration)
    {
        doors = doorConfiguration;

        topDoor.SetActive(false);
        topBlocker.SetActive(!doors[0]);
        topSpawnPoint.SetActive(doors[0]);

        bottomDoor.SetActive(false);
        bottomBlocker.SetActive(!doors[1]);
        bottomSpawnPoint.SetActive(doors[1]);

        leftDoor.SetActive(false);
        leftBlocker.SetActive(!doors[2]);
        leftSpawnPoint.SetActive(doors[2]);

        rightDoor.SetActive(false);
        rightBlocker.SetActive(!doors[3]);
        rightSpawnPoint.SetActive(doors[3]);

        mapX = x;
        mapY = y;
        GameManager.instance.AddRoom(x, y, doorConfiguration);
    }

    public void ToggleDoors(bool closed)
    {
        if (doors[0]) { topDoor.SetActive(closed); }
        if (doors[1]) { bottomDoor.SetActive(closed); }
        if (doors[2]) { leftDoor.SetActive(closed); }
        if (doors[3]) { rightDoor.SetActive(closed); }
    }

    public void SpawnEnemies(List<GameObject> enemiesPool)
    {
        if (enemiesPool.Count > 0)
        {
            enemiesSpawned = true;
            int rand = Random.Range(0, 4);
            for (int i = 0; i <= rand; i++)
            {
                GameObject randomEnemy = enemiesPool[Random.Range(0, enemiesPool.Count)];
                int randomX = Random.Range(-8, 9);
                int randomZ = Random.Range(-8, 9);
                Vector3 enemyPosition = transform.position + new Vector3(randomX, 1.0f, randomZ);
                GameObject enemy = Instantiate(randomEnemy, enemyPosition, transform.rotation);
                enemies.Add(enemy);
            }
        }
    }

    public void Update()
    {
        if (enemiesSpawned)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    return;
                }
            }
            ToggleDoors(false);

            if (!upgradeSpawned)
            {
                GameManager.instance.roomsCleared += 1;
                GameObject newUpgrade = Instantiate(upgrade);
                newUpgrade.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                upgradeSpawned = true;
            }
        }
        
    }

    public void Enter()
    {
        if (!entered)
        {
            StartCoroutine(SpawnDelay());
            ToggleDoors(true);
            entered = true;
            GameManager.instance.SetRoomSeen(mapX, mapY);
            sfxManager.playDoorLock();
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.instance.roomsCleared <= GameManager.instance.roomsTotal/3)
        {
            SpawnEnemies(enemiesPoolEasy);
        }
        else if (GameManager.instance.roomsCleared > GameManager.instance.roomsTotal/3 && GameManager.instance.roomsCleared <= 2*GameManager.instance.roomsTotal/3)
        {
            SpawnEnemies(enemiesPoolMedium);
        }
        else if (GameManager.instance.roomsCleared > 2*GameManager.instance.roomsTotal/3 && GameManager.instance.roomsCleared <= GameManager.instance.roomsTotal)
        {
            SpawnEnemies(enemiesPoolHard);
        }
    }
}
