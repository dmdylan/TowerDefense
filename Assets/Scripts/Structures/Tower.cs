using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Structure
{
    private float currentHealth;
    private TowerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = structureStats.BaseHealth;
        SetState(new TowerDetectingState(this));
        currentState = towerState;
        StartCoroutine(ProximityCheck());
        StartCoroutine(towerState.UpdateState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != towerState)
        {
            StartCoroutine(towerState.UpdateState());
            currentState = towerState;
        }
    }

    IEnumerator ProximityCheck()
    {
        //while there are no objects to attack in attack range
        while (attackableInRange.Equals(false))
        {
            yield return new WaitForSeconds(.1f);
            //Checks for any structures in attack range, if there are then it 
            //changes to attack state and sets bool to true
            if (Physics.CheckSphere(transform.position, structureStats.BaseRange, layerMask))
            {
                Debug.Log("enemy in range");
                towerState.ExitState();
                attackableInRange = true;
                SetState(new TowerAttackingState(this));
            }
        }

        //while there is an object in range to attack
        while (attackableInRange.Equals(true))
        {
            yield return new WaitForSeconds(.1f);

            //checks if there are no structures in range to attack (the structure was destroyed)
            //if so it changes back to pathing state and sets bool to false
            if (Physics.CheckSphere(transform.position, structureStats.BaseRange, layerMask).Equals(false))
            {
                towerState.ExitState();
                attackableInRange = false;
                SetState(new TowerDetectingState(this));
            }
        }

        //Restarts the coroutine upon entering pathing state again
        StartCoroutine(ProximityCheck());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, structureStats.BaseRange);
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
