using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyWaveManager : MonoBehaviour
{
    enum SpawningState { Spawning, NothingLeftToSpawn, BetweenWaves };
    public Wave[] waves;

    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private Spawner[] spawners = null;
    int spawnerNumber = 0;

    private bool isSpawning = false;
    SpawningState state = SpawningState.Spawning;
    int currentWave = 1;

    private void OnEnable()
    {
        GameEvents.Instance.OnWaveEnded += Instance_OnWaveEnded;
        GameEvents.Instance.OnWaveStarted += Instance_OnWaveStarted;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnWaveEnded -= Instance_OnWaveEnded;
        GameEvents.Instance.OnWaveStarted -= Instance_OnWaveStarted;
    }

    private void Instance_OnWaveStarted()
    {
        throw new System.NotImplementedException();
    }

    private void Instance_OnWaveEnded()
    {
        isSpawning = false;
        currentWave++;
    }

    // Start is called before the first frame update
    void Start()
    {
        //maybe create a better way to identify enemies...
        //use their ID?
        foreach(GameObject enemy in enemyPrefabs)
        {
            var numberToPool = enemy.GetComponent<Enemy>().baseEnemyStats.EnemyCountValue;

            if(numberToPool == 25)
                PoolManager.Instance.CreatePool(enemy, 2);
            else if(numberToPool == 20)
                PoolManager.Instance.CreatePool(enemy, 10);
            else if (numberToPool == 15)
                PoolManager.Instance.CreatePool(enemy, 20);
            else
                PoolManager.Instance.CreatePool(enemy, 30);
        }

        //Don't call it at the start, have a game event set up for player to start when ready
        //just calling at the start for testing purposes
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

    public Transform enemy;

    public float timeBetweenWaves = 5f;
    public float countDown = 2f;

    //public float searchCountdown = 1f; 

    private List<Transform> enemies = new List<Transform>();
    private int waveIndex = 0;

    // this replaces your Update method
    private IEnumerator RunSpawner()
    {
        // first time wait 2 seconds
        yield return new WaitForSeconds(countDown);

        // run this routine infinite
        while (true)
        {
            state = SpawningState.Spawning;

            // do the spawning and at the same time wait until it's finished
            yield return SpawnWave();

            state = SpawningState.NothingLeftToSpawn;

            // wait until all enemies died (are destroyed)
            yield return new WaitWhile(EnemyisAlive);

            state = SpawningState.BetweenWaves;
    
        // wait 5 seconds
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private bool EnemyisAlive()
    {
        // uses Linq to filter out null (previously detroyed) entries
        enemies = enemies.Where(e => e != null).ToList();
        return enemies.Count > 0;
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemy, transform.position, transform.rotation));
    }
}
