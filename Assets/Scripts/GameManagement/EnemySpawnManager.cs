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

    //sometimes has issues assigning objective to enemy
    void SetEnemyObjective(GameObject enemyPrefab, Spawner enemySpawner)
    {
        //if (!enemyPrefab.GetComponent<Enemy>() || enemySpawner.Objective.Equals(null))
        //    return;
        //else
            enemyPrefab.GetComponent<Enemy>().Objective = enemySpawner.Objective.position;        
    }

    //sometimes has issues assigning objective to enemy
    public void SpawnEnemies(GameObject enemyPrefab, Spawner enemySpawner)
    {
        PoolManager.Instance.ReuseObject(enemyPrefab, enemySpawner.transform.position, enemySpawner.transform.rotation);
        SetEnemyObjective(enemyPrefab, enemySpawner);
    }
}
