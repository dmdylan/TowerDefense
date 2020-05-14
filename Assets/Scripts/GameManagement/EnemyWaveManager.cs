using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWaveManager : MonoBehaviour
{
    enum SpawningState { Spawning, Waiting, Counting };
    public Wave[] waves;

    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private Spawner[] spawners = null;
    int spawnerNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(enemyPrefabs[0], 20);
        StartCoroutine(SpawnEnemies(enemyPrefabs[0], spawners));
    }

    // Update is called once per frame
    void Update()
    {
        spawnerNumber = Random.Range(0, spawners.Length);
    }

    IEnumerator SpawnEnemies(GameObject enemy, Spawner[] spawnLocation)
    {
        yield return new WaitForSeconds(2);
        EnemySpawnManager.Instance.SpawnEnemies(enemy, spawnLocation[spawnerNumber]);
        StartCoroutine(SpawnEnemies(enemy, spawnLocation));
    }
}
