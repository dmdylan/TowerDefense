using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barricade : Structure
{
    private float currentHealth = 0;


    private void Start()
    {
        currentHealth = BaseHealth;
        //Debug.Log(baseHealth);
        //Debug.Log(baseDamage);
        //Debug.Log(baseRange);
        //Debug.Log(baseAttackRate);
        //Debug.Log(resourceBuildCost);
        //Debug.Log(resourceUpgradeCost);
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    public override IEnumerator Repair()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
