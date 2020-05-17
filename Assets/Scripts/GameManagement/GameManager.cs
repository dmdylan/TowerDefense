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

    private float currentThreatLevel = 0;
    public float CurrentThreatLevel => currentThreatLevel;

    [SerializeField] private LevelLength levelLength = null;
    public LevelLength LevelLength => levelLength;

    [SerializeField] private GameObject[] enemyPrefabs = null;
    public GameObject[] EnemyPrefabs => enemyPrefabs;
    #endregion


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
        //maybe create a better way to identify enemies...
        //use their ID?
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
    }

    // Start is called before the first frame update
    void Start()
    {
        currentThreatLevel = (float)LevelDifficulty.Difficulty;
    }

    private void Update()
    {
        
    }

    private void Instance_OnWaveStarted()
    {
        throw new System.NotImplementedException();
    }

    private void Instance_OnWaveEnded()
    {
        currentThreatLevel++;
    }
}
