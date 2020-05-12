using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelDifficulty levelDifficulty;
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private Spawner[] spawners = null;
    int spawnPosition = 5;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(enemyPrefabs[0], 20);
        StartCoroutine(SpawnEnemies(enemyPrefabs[0], spawners[spawnPosition]));
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = Random.Range(0, spawners.Length);       
    }

    IEnumerator SpawnEnemies(GameObject enemy, Spawner spawnLocation)
    {
        yield return new WaitForSeconds(2);
        EnemySpawnManager.Instance.SpawnEnemies(enemy, spawnLocation);
        StartCoroutine(SpawnEnemies(enemy, spawnLocation));
    }
}
