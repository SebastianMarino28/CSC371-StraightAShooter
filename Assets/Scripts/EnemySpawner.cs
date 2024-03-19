using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    intro,
    easy,
    medium,
    hard,
    midterm1,
    midterm2
}

public class EnemySpawner : MonoBehaviour
{
    // Intro enemies pool should include all easy enemies
    public List<GameObject> enemiesPoolIntro = new List<GameObject>();

    // These other enemy pools can be customized
    public List<GameObject> enemiesPoolEasy = new List<GameObject>();
    public List<GameObject> enemiesPoolMedium = new List<GameObject>();
    public List<GameObject> enemiesPoolHard = new List<GameObject>();
    public List<GameObject> enemiesPoolMidterm1 = new List<GameObject>();
    public List<GameObject> enemiesPoolMidterm2 = new List<GameObject>();

    public ParticleSystem spawnEffect;

    public GameObject spawnedEnemy;

    public IEnumerator SpawnEnemy(Difficulty difficulty)
    {
        GameObject enemy;
        switch(difficulty)
        {
            case Difficulty.intro:
                enemy = enemiesPoolIntro[GameManager.instance.roomsCleared];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            case Difficulty.easy:
                enemy = enemiesPoolEasy[Random.Range(0, enemiesPoolEasy.Count)];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            case Difficulty.medium:
                enemy = enemiesPoolMedium[Random.Range(0, enemiesPoolMedium.Count)];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            case Difficulty.hard:
                enemy = enemiesPoolHard[Random.Range(0, enemiesPoolHard.Count)];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            case Difficulty.midterm1:
                enemy = enemiesPoolMidterm1[Random.Range(0, enemiesPoolMidterm1.Count)];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            case Difficulty.midterm2:
                enemy = enemiesPoolMidterm2[Random.Range(0, enemiesPoolMidterm2.Count)];
                spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
                break;
            default:
                break;
        }
        spawnedEnemy.SetActive(false);
        ParticleSystem effect = Instantiate(spawnEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(.25f);
        effect.Stop(true);
        yield return new WaitForSeconds(2F);
        spawnedEnemy.SetActive(true);
    }
}
