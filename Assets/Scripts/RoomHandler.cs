using UnityEngine;
using System.Collections.Generic;

public class RoomHandler : MonoBehaviour
{
    // 4 doors: 0 = top, 1 = bottom, 2 = left, 3 = right. If true, door in that location. Else, blocker.
    private bool[] doors;
    public List<GameObject> enemiesPool = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private bool entered = false;

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


    public void ConfigureRoom(bool[] doorConfiguration)
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

        SpawnEnemies();
    }

    public void ToggleDoors(bool closed)
    {
        if (doors[0]) { topDoor.SetActive(closed); }
        if (doors[1]) { bottomDoor.SetActive(closed); }
        if (doors[2]) { leftDoor.SetActive(closed); }
        if (doors[3]) { rightDoor.SetActive(closed); }
    }

    public void SpawnEnemies()
    {
        if (enemiesPool.Count > 0)
        {
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
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                return;
            }
        }
        ToggleDoors(false);
    }

    public void Enter()
    {
        if (!entered)
        {
            ToggleDoors(true);
            entered = true;
        }
    }
}
