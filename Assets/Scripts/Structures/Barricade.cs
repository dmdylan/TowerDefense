using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barricade : Structure
{
    private float currentHealth = 0;

    private void Start()
    {
        currentHealth = structureStats.BaseHealth;
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
