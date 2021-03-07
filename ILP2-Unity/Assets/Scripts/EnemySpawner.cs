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

    // Update is called once per frame
    void Update()
    {
        checkSpawn();
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
            spawnInterval *= 0.9f;//reduce spawn interval by 10%
        }
    }

    void SpawnEnemy()
    {
        prevSpawnTime = Time.time;

        Vector3 position;

        int randNum = Random.Range(0, 2);

        if (randNum == 0) {
            //spawn at top of screen
            position = new Vector3(Random.Range(-10f, 10f), 5.5f, 0);
        }
        else {
            //spawn at bottom of screen
            position = new Vector3(Random.Range(-10f, 10f), -5.5f, 0);
        }

        //creating enemy
        EnemyController spawnedEnemy = Instantiate(Enemy, position, Quaternion.identity);

        //giving enemy reference to player
        spawnedEnemy.setRefs(player, gm);

    }
}
