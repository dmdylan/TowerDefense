using StateStuff;
using System.Collections;
using UnityEngine;

public abstract class Structure : TowerStateMachine, IDamageable
{
    public StructureStats structureStats;
    public readonly int layerMask = 1 << 10;
    public bool attackableInRange;
    public Transform firePoint;

    public abstract IEnumerator Repair();
    public abstract void TakeDamage(float damageAmount);
}
