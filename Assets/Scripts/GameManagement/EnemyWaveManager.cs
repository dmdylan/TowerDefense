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

        foreach(Wave[] wave in waves)
        {
            //wave[]= GenerateNewWave(enemyPrefabs, GameManager.Instance.CurrentThreatLevel);
        }

        Debug.Log(waves);
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

    private Wave GenerateNewWave(GameObject[] enemyPool, float currentThreatLevel)
    {
        List<GameObject> possibleEnemiesThatCanSpawn = new List<GameObject>();
        List<GameObject> enemiesThatWillSpawn = new List<GameObject>();

        int totalEnemyCountValue = Mathf.RoundToInt(100 * currentThreatLevel * (int)GameManager.Instance.LevelDifficulty.Difficulty);

        //Goes through pool of all enemies in level and adds them to potential spawn list based on current wave threat level
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.GetComponent<Enemy>().baseEnemyStats.ThreatLevel <= currentThreatLevel)
            {
                possibleEnemiesThatCanSpawn.Add(enemy);
            }
        }

        //Add random enemies to list of things to spawn
        for (int i = 0; i <= totalEnemyCountValue;)
        {
            int enemyToAddPosition = Random.Range(0, possibleEnemiesThatCanSpawn.Count);
            GameObject enemyToAdd = possibleEnemiesThatCanSpawn[enemyToAddPosition];

            //Check if the list has the enemy type limit
            //Will probably have to refactor this, has to iterate through the list everytime
            if (CheckEnemyListCount(enemyToAdd, enemiesThatWillSpawn) < enemyToAdd.GetComponent<Enemy>().baseEnemyStats.AllowedPerWave)
                enemiesThatWillSpawn.Add(enemyToAdd);
            else
                possibleEnemiesThatCanSpawn.Remove(enemyToAdd);

            i += enemyToAdd.GetComponent<Enemy>().baseEnemyStats.EnemyCountValue;
        }

        var newWave = new Wave(enemiesThatWillSpawn.ToArray());
        return newWave;
    }

    //Takes the enemy game object and the current enemy spawn list
    //Adds up how many times it is in that list and returns it
    int CheckEnemyListCount(GameObject enemyToAdd, List<GameObject> enemiesThatWillSpawn)
    {
        int i = 0;

        foreach (GameObject enemy in enemiesThatWillSpawn)
        {
            if (enemy.Equals(enemyToAdd))
                i++;
        }

        return i;
    }

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
