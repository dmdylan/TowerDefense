﻿using System.Collections;
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
        //if (!enemyPrefab.GetComponent<NPCBehavior>())
        //    return;
        //else
            enemyPrefab.GetComponent<NPCBehavior>().objective = enemySpawner.Objective;        
    }

    public void SpawnEnemies(GameObject enemyPrefab, Spawner enemySpawner)
    {
        SetEnemyObjective(enemyPrefab, enemySpawner);
        PoolManager.Instance.ReuseObject(enemyPrefab, enemySpawner.transform.position, enemySpawner.transform.rotation);
        Debug.Log(enemySpawner.transform.position);
    }
}
