using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Create Instance
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    #region Properties
    [SerializeField] private LevelDifficulty levelDifficulty = null;
    public LevelDifficulty LevelDifficulty => levelDifficulty;

    public float CurrentThreatLevel { get; private set; } = 0;

    [SerializeField] private LevelLength levelLength = null;
    public LevelLength LevelLength => levelLength;

    [SerializeField] private GameObject[] enemyPrefabs = null;
    public GameObject[] EnemyPrefabs => enemyPrefabs;
    #endregion

    public GameObject towerProjectile;

    private void OnEnable()
    {
        GameEvents.Instance.OnWaveStarted += Instance_OnWaveStarted;
        GameEvents.Instance.OnWaveEnded += Instance_OnWaveEnded;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnWaveStarted -= Instance_OnWaveStarted;
        GameEvents.Instance.OnWaveEnded -= Instance_OnWaveEnded;
    }

    private void Awake()
    {
        //Create pool of enemies based on enemy type
        foreach (GameObject enemy in enemyPrefabs)
        {
            var numberToPool = enemy.GetComponent<Enemy>().baseEnemyStats.EnemyType;

            if (numberToPool.Equals(EnemyType.Boss))
                PoolManager.Instance.CreatePool(enemy, 2);
            else if (numberToPool.Equals(EnemyType.Large))
                PoolManager.Instance.CreatePool(enemy, 10);
            else if (numberToPool.Equals(EnemyType.Medium))
                PoolManager.Instance.CreatePool(enemy, 20);
            else
                PoolManager.Instance.CreatePool(enemy, 30);
        }

        PoolManager.Instance.CreatePool(towerProjectile, 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentThreatLevel = (float)LevelDifficulty.Difficulty;
    }

    private void Update()
    {
        
    }

    private void Instance_OnWaveStarted()
    {
        Debug.Log("Wave started");
    }

    private void Instance_OnWaveEnded()
    {
        CurrentThreatLevel++;
    }
}
