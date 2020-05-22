using StateStuff;
using System.Collections;

public abstract class Structure : TowerStateMachine, IDamageable
{
    public StructureStats structureStats;
    public readonly int layerMask = 1 << 10;
    public bool attackableInRange;

    public float BaseHealth => structureStats.BaseHealth;
    public float BaseDamage => structureStats.BaseDamage;
    public float BaseRange => structureStats.BaseRange;
    public float BaseAttackRate => structureStats.BaseAttackRate;
    public float ResourceBuildCost => structureStats.ResourceBuildCost;
    public float ResourceUpgradeCost => structureStats.ResourceUpgradeCost;

    public abstract IEnumerator Repair();
    public abstract void TakeDamage(float damageAmount);
}
