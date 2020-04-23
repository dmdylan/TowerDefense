using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barricade : Structure
{
    public override void TakeDamage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        Debug.Log(baseHealth);
        Debug.Log(baseDamage);
        Debug.Log(baseRange);
        Debug.Log(baseAttackRate);
        Debug.Log(resourceBuildCost);
        Debug.Log(resourceUpgradeCost);
    }
}
