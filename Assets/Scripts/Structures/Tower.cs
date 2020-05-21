using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Structure
{
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = structureStats.BaseHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ProximityCheck()
    {
        //while there are no objects to attack in attack range
        while (attackableInRange.Equals(false))
        {
            yield return new WaitForSeconds(.25f);
            //Checks for any structures in attack range, if there are then it 
            //changes to attack state and sets bool to true
            if (Physics.CheckSphere(transform.position, structureStats.BaseRange, layerMask))
            {
                state.ExitState();
                SetState(new TowerAttackingState(this));
                attackableInRange = true;
            }
        }

        //while there is an object in range to attack
        while (attackableInRange.Equals(true))
        {
            yield return new WaitForSeconds(.25f);

            //checks if there are no structures in range to attack (the structure was destroyed)
            //if so it changes back to pathing state and sets bool to false
            if (Physics.CheckSphere(transform.position, structureStats.BaseRange, layerMask).Equals(false))
            {
                state.ExitState();
                SetState(new TowerDetectingState(this));
                attackableInRange = false;
            }
        }

        //Restarts the coroutine upon entering pathing state again
        StartCoroutine(ProximityCheck());
    }

    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
    public override IEnumerator Repair()
    {
        throw new System.NotImplementedException();
    }
}
