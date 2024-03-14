using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomHandler : MonoBehaviour
{
    // 4 doors: 0 = top, 1 = bottom, 2 = left, 3 = right. If true, door in that location. Else, blocker.
    private bool[] doors;
    
    public List<GameObject> spawnLocations = new List<GameObject>();

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

    public void SpawnEnemies(Difficulty difficulty)
    {
        if (spawnLocations.Count > 0)
        {
            int enemyCount = 0;
            switch (difficulty)
            {
                case Difficulty.intro:
                    enemyCount = 1;
                    break;
                case Difficulty.easy:
                    enemyCount = 2;
                    break;
                case Difficulty.medium:
                    enemyCount = Random.Range(2, spawnLocations.Count);
                    break;
                case Difficulty.hard:
                    enemyCount = Random.Range(2, spawnLocations.Count + 1);
                    break;
            }
            List<int> spawnLocationIds = new List<int>();
            while (spawnLocationIds.Count < enemyCount)
            {
                int rand = Random.Range(0, 4);
                if (!spawnLocationIds.Contains(rand))
                {
                    spawnLocationIds.Add(rand);
                }
            }
            for (int i = 0; i < spawnLocationIds.Count; i++)
            { 
                EnemySpawner enemySpawner = spawnLocations[spawnLocationIds[i]].GetComponent<EnemySpawner>();
                StartCoroutine(enemySpawner.SpawnEnemy(difficulty));
                enemies.Add(enemySpawner.spawnedEnemy);
            }
            
            enemiesSpawned = true;
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
            if (GameManager.instance.roomsCleared < 4)
            {
                SpawnEnemies(Difficulty.intro);
            }
            else if (GameManager.instance.roomsCleared <= GameManager.instance.roomsTotal / 3)
            {
                SpawnEnemies(Difficulty.easy);
            }
            else if (GameManager.instance.roomsCleared > GameManager.instance.roomsTotal / 3 && GameManager.instance.roomsCleared <= 2 * GameManager.instance.roomsTotal / 3)
            {
                SpawnEnemies(Difficulty.medium);
            }
            else if (GameManager.instance.roomsCleared > 2 * GameManager.instance.roomsTotal / 3 && GameManager.instance.roomsCleared <= GameManager.instance.roomsTotal)
            {
                SpawnEnemies(Difficulty.hard);
            }
            ToggleDoors(true);
            entered = true;
            GameManager.instance.SetRoomSeen(mapX, mapY);
            sfxManager.playDoorLock();
        }
    }
}
