using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int maxEnemies;

    float prevSpawnTime = -1;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject Enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        Instantiate(Enemy, position, Quaternion.identity);

    }
}
