using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int maxEnemies;

    float prevSpawnTime = -1;

    [SerializeField]
    float spawnInterval;

    float prevIncreaseRateTime = -1;

    [SerializeField]
    float spawnIncreaseRateInterval;

    [SerializeField]
    EnemyController Enemy;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameManager gm;

    void Start()
    {
        prevSpawnTime = Time.time - 1;
        prevIncreaseRateTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        checkSpawn();
        checkIncreaseRate();
    }

    void checkSpawn()
    {
        if (Time.time - prevSpawnTime >= spawnInterval) {
            SpawnEnemy();
        }
    }

    void checkIncreaseRate()
    {
        if (Time.time - prevIncreaseRateTime >= spawnIncreaseRateInterval) {
            spawnInterval *= 0.85f;//reduce spawn interval by 15% of it's current value
            //spawnInterval = spawnInterval >= 0.2f ? spawnInterval - 0.3f : 0.2f;

            prevIncreaseRateTime = Time.time;
        }
    }

    void SpawnEnemy()
    {
        prevSpawnTime = Time.time;

        Vector3 position;

        int randNum = Random.Range(0, 4);

        if (randNum == 0) {
            //spawn at top of screen
            position = new Vector3(Random.Range(-12.1f, 12.1f), -7.6f, 0);
        }
        else if (randNum == 1) {
            //spawn at bottom of screen
            position = new Vector3(Random.Range(-12.1f, 12.1f), -7.6f, 0);
        }
        else if (randNum == 2) {
            //spawn at right of screen
            position = new Vector3(12.1f, Random.Range(-7.6f, 7.6f), 0);
        }
        else {
            //spawn at left of screen
            position = new Vector3(-12.1f, Random.Range(-7.6f, -7.6f), 0);
        }

        //creating enemy
        EnemyController spawnedEnemy = Instantiate(Enemy, position, Quaternion.identity);

        //giving enemy reference to player
        spawnedEnemy.setRefs(player, gm);

    }
}
