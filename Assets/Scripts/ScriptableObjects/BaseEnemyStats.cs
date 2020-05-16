using UnityEngine;

public enum AttackType { Melee, Ranged }
public enum EnemyType { Small, Medium, Large, Boss }

[CreateAssetMenu (fileName = "New Enemy Stats", menuName = "Enemy/New Enemy Stats")]
public class BaseEnemyStats : ScriptableObject
{
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float maxHealth = 0;
    [SerializeField] private float damage = 0;
    [SerializeField] private AttackType attackType = AttackType.Melee;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float attackRate = 1;
    [SerializeField] private int pointValue = 0;
    [SerializeField] private float timeBetweenSpawn = 0;
    [SerializeField] private int enemyCountValue = 0;
    [SerializeField] private float threatLevel = 0;
    [SerializeField] private int enemyID = 0;
    [SerializeField] private EnemyType enemyType = EnemyType.Small;
    private int allowedPerWave = 0;

    public float MovementSpeed => movementSpeed;
    public float MaxHealth => maxHealth;
    public float Damage => damage;
    public AttackType AttackType => attackType;
    public float AttackRange => attackRange;
    public float AttackRate => attackRate;
    public int PointValue => pointValue;
    public float TimeBetweenSpawn => timeBetweenSpawn;
    public int EnemyCountValue => enemyCountValue;
    public float ThreatLevel => threatLevel;
    public int EnemyID => enemyID;
    public EnemyType EnemyType => enemyType;
    public int AllowedPerWave
    {
        get
        {
            if (EnemyType.Equals(EnemyType.Small) || EnemyType.Equals(EnemyType.Medium))
            {
                allowedPerWave = int.MaxValue;
            }
            else if (EnemyType.Equals(EnemyType.Large))
            {
                allowedPerWave = 100;
            }
            else if (EnemyType.Equals(EnemyType.Boss))
            {
                allowedPerWave = 5;
            }

            return allowedPerWave;
        }
    }
}
