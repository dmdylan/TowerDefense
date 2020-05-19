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
    public Wave[] waves;
    int enemiesInWave;

    [SerializeField] private Spawner[] spawners = null;
    int spawnerNumber = 0;
    GameObject[] enemyPrefabs = null;

    private bool waveOngoing = false;
    SpawningState state = SpawningState.Spawning;
    private int currentWave;

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
        currentWave = 1;

        if(GameManager.Instance.LevelLength.Length.Equals(Length.Short))
            waves = new Wave[5];
        else if (GameManager.Instance.LevelLength.Length.Equals(Length.Medium))
            waves = new Wave[7];
        else
            waves = new Wave[10];

        //Generate the first wave
        waves[0] = GenerateNewWave(enemyPrefabs, GameManager.Instance.CurrentThreatLevel);
        
        //Debug contents of the wave
        //for (int i = 0; i < waves[0].EnemiesToSpawn.Length; i++)
        //{ 
        //    Debug.Log(waves[0].EnemiesToSpawn[i]);
        //}

        //Don't call it at the start, have a game event set up for player to start when ready
        //just calling at the start for testing purposes
        StartCoroutine(SpawnEnemies(waves[currentWave-1], spawners));
    }

    // Update is called once per frame
    void Update()
    {
        spawnerNumber = Random.Range(0, spawners.Length);
    }

    // this replaces your Update method
    private IEnumerator RunSpawner()
    {    
        // run this routine infinite
        while (waveOngoing)
        {
            state = SpawningState.Spawning;
    
            // do the spawning and at the same time wait until it's finished
            yield return StartCoroutine(SpawnEnemies(waves[currentWave-1], spawners));
    
            state = SpawningState.NothingLeftToSpawn;
    
            // wait until all enemies died (are destroyed)
            yield return new WaitWhile(() => enemiesInWave > 0);
    
            state = SpawningState.BetweenWaves;
        }
    }

    //Need a way to check and hold timer of most recent enemy spawn to prevent quick spawning of said enemy
    //or do we just not care about that?
    //Needs to take in a wave instead of an enemy?
    IEnumerator SpawnEnemies(Wave wave, Spawner[] spawnLocation)
    {
        for (int i = 0; i < wave.EnemiesToSpawn.Length; i++)
        {
            EnemySpawnManager.Instance.SpawnEnemies(wave.EnemiesToSpawn[i], spawnLocation[spawnerNumber]);
            yield return new WaitForSeconds(wave.EnemiesToSpawn[i].GetComponent<Enemy>().baseEnemyStats.TimeBetweenSpawn);
        }
    }

    private Wave GenerateNewWave(GameObject[] enemyPool, float currentThreatLevel)
    {
        enemiesInWave = 0;

        List<GameObject> possibleEnemiesThatCanSpawn = new List<GameObject>();
        List<GameObject> enemiesThatWillSpawn = new List<GameObject>();

        //Changes the number of possible enemies that can be added to the wave
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
        for (int i = 0; i < totalEnemyCountValue;)
        {
            int enemyToAddPosition = Random.Range(0, possibleEnemiesThatCanSpawn.Count);
            GameObject enemyToAdd = possibleEnemiesThatCanSpawn[enemyToAddPosition];

            //Check if the list has the enemy type limit
            //Will probably have to refactor this, has to iterate through the list everytime
            if (CheckEnemyListCount(enemyToAdd, enemiesThatWillSpawn) < enemyToAdd.GetComponent<Enemy>().baseEnemyStats.AllowedPerWave)
            {
                enemiesThatWillSpawn.Add(enemyToAdd);
                enemiesInWave++;
            }
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
        waveOngoing = true;
    }

    private void Instance_OnWaveEnded()
    {
        waveOngoing = false;
        currentWave++;
        waves[currentWave] = GenerateNewWave(enemyPrefabs, GameManager.Instance.CurrentThreatLevel);
    }
    #endregion
}
