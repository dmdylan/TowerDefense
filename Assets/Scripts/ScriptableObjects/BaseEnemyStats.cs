using UnityEngine;

public enum AttackType { Melee, Ranged }

[CreateAssetMenu (fileName = "New Enemy Stats", menuName = "Enemy/New Enemy Stats")]
public class BaseEnemyStats : ScriptableObject
{
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float maxHealth = 0;
    [SerializeField] private float damage = 0;
    [SerializeField] private AttackType attackType = AttackType.Melee;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float attackRate = 1;

    public float MovementSpeed => movementSpeed;
    public float MaxHealth => maxHealth;
    public float Damage => damage;
    public AttackType AttackType => attackType;
    public float AttackRange => attackRange;
    public float AttackRate => attackRate;
}
