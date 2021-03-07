using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    byte maxEnemyPool;

    GameObject[] Enemies;
    int[] emptyIndexes;//stores list of inactive objects indexes

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new GameObject[maxEnemyPool];
        emptyIndexes = new int[maxEnemyPool];

        foreach (GameObject enemy in Enemies) {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void destroyObject(int index)
    {
        Enemies[index].SetActive(false);
    }

    void spawnObject()
    {

    }
}
