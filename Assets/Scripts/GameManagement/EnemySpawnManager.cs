using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    static EnemySpawnManager instance;
    public static EnemySpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemySpawnManager>();
            }
            return instance;
        }
    }

    void SetEnemyObjective(GameObject enemyPrefab, Spawner enemySpawner)
    {
        //if (!enemyPrefab.GetComponent<Enemy>())
        //    return;
        //else
            enemyPrefab.GetComponent<Enemy>().objective = enemySpawner.Objective;        
    }

    //sometimes has issues assigning objective to enemy
    public void SpawnEnemies(GameObject enemyPrefab, Spawner enemySpawner)
    {
        SetEnemyObjective(enemyPrefab, enemySpawner);
        PoolManager.Instance.ReuseObject(enemyPrefab, enemySpawner.transform.position, enemySpawner.transform.rotation);
    }
}
