using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyWaveManager : MonoBehaviour
{
    #region Instance
    static EnemyWaveManager instance;

    public EnemyWaveManager Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyWaveManager>();
            }
            return instance;
        }
    }
    #endregion

    enum SpawningState { Spawning, NothingLeftToSpawn, BetweenWaves };
    public Wave[][] waves;

    [SerializeField] private Spawner[] spawners = null;
    int spawnerNumber = 0;
    GameObject[] enemyPrefabs = null;

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

    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabs = GameManager.Instance.EnemyPrefabs;

        if(GameManager.Instance.LevelLength.Length.Equals(0))
            waves = new Wave[5][];
        else if (GameManager.Instance.LevelLength.Length.Equals(1))
            waves = new Wave[7][];
        else
            waves = new Wave[10][];

        //Don't call it at the start, have a game event set up for player to start when ready
        //just calling at the start for testing purposes
        StartCoroutine(SpawnEnemies(enemyPrefabs[0], spawners));
    }

    // Update is called once per frame
    void Update()
    {
        spawnerNumber = Random.Range(0, spawners.Length);
    }

    //Need a way to check and hold timer of most recent enemy spawn to prevent quick spawning of said enemy
    //or do we just not care about that?
    IEnumerator SpawnEnemies(GameObject enemy, Spawner[] spawnLocation)
    {
        yield return new WaitForSeconds(2);
        EnemySpawnManager.Instance.SpawnEnemies(enemy, spawnLocation[spawnerNumber]);
        StartCoroutine(SpawnEnemies(enemy, spawnLocation));
    }

    // this replaces your Update method
    //private IEnumerator RunSpawner()
    //{
    //    // first time wait 2 seconds
    //    yield return new WaitForSeconds(countDown);
    //
    //    // run this routine infinite
    //    while (true)
    //    {
    //        state = SpawningState.Spawning;
    //
    //        // do the spawning and at the same time wait until it's finished
    //        yield return SpawnWave();
    //
    //        state = SpawningState.NothingLeftToSpawn;
    //
    //        // wait until all enemies died (are destroyed)
    //        yield return new WaitWhile(EnemyisAlive);
    //
    //        state = SpawningState.BetweenWaves;
    //
    //    // wait 5 seconds
    //        yield return new WaitForSeconds(timeBetweenWaves);
    //    }
    //}

    #region Events
    private void Instance_OnWaveStarted()
    {
        isSpawning = true;
        state = SpawningState.Spawning;
    }

    private void Instance_OnWaveEnded()
    {
        isSpawning = false;
        state = SpawningState.NothingLeftToSpawn;
        currentWave++;
    }
    #endregion
}
