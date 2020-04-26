using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barricade : Structure
{
    private float currentHealth = 0;

    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    private void Start()
    {
        currentHealth = baseHealth;
        //Debug.Log(baseHealth);
        //Debug.Log(baseDamage);
        //Debug.Log(baseRange);
        //Debug.Log(baseAttackRate);
        //Debug.Log(resourceBuildCost);
        //Debug.Log(resourceUpgradeCost);
    }

    private void Update()
    {
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
