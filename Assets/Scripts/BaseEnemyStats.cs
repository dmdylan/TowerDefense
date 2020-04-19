using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Melee, Ranged }

[CreateAssetMenu (fileName = "New Enemy Stats", menuName = "Enemy/New Enemy Stats")]
public class BaseEnemyStats : ScriptableObject
{
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float health = 0;
    [SerializeField] private AttackType attackType = AttackType.Melee;
    [SerializeField] private float attackRange = 1;

    public float MovementSpeed => movementSpeed;
    public float Health => health;
    public AttackType AttackType => attackType;
    public float AttackRange => attackRange;
}
