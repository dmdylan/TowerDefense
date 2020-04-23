using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour, IDamageable
{
    public StructureStats structureStats;

    protected float baseHealth = 0;
    protected float baseDamage = 0;
    protected float baseRange = 0;
    protected float baseAttackRate = 0;
    protected float resourceBuildCost = 0;
    protected float resourceUpgradeCost = 0;

    protected void OnEnable()
    {
        baseHealth = structureStats.BaseHealth;
        baseDamage = structureStats.BaseDamage;
        baseRange = structureStats.BaseRange;
        baseAttackRate = structureStats.BaseAttackRate;
        resourceBuildCost = structureStats.ResourceBuildCost;
        resourceUpgradeCost = structureStats.ResourceUpgradeCost;
    }

    protected virtual IEnumerator Attack()
    {
        return null;
    }

    IEnumerator Repair()
    {
        return null;
    }

    public abstract void TakeDamage(float damageAmount);
}
