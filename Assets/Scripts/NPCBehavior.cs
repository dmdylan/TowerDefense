using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using StateStuff;

public class NPCBehavior : NPCStateMachine
{
    public Transform objective = null;
    public BaseEnemyStats baseEnemyStats;
    public NavMeshAgent navMeshAgent;
    //bool switchStates = false; //for debugging
    bool attackableInRange = false;
    readonly int layerMask = 1 << 8;

    
    //TODO: Setup way in code for object to be auto assigned to enemy through code

    // Start is called before the first frame update
    void Start()
    {
       navMeshAgent = this.GetComponent<NavMeshAgent>();
       SetState(new PathingState(this));
       state.EnterState();
       StartCoroutine(ProximityCheck());

       //TODO: Make it so update coroutine is called repeatedly and not just once or just swap back to method
       StartCoroutine(state.UpdateState());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        //state.UpdateState();
        
    }

    IEnumerator ProximityCheck()
    {
        //while there are no objects to attack in attack range
        while (attackableInRange.Equals(false))
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Checking for structures");
            //Checks for any structures in attack range, if there are then it 
            //changes to attack state and sets bool to true
            if(Physics.CheckSphere(transform.position, 3, layerMask))
            {
                state.ExitState();
                SetState(new AttackingState(this));
                state.EnterState();
                attackableInRange = true;
                Debug.Log("Found structure");
            }
            Debug.Log("No structres found");
        }

        //while there is an object in range to attack
        while (attackableInRange.Equals(true))
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Attacking structure");

            //checks if there are no structures in range to attack (the structure was destroyed)
            //if so it changes back to pathing state and sets bool to false
            if (Physics.CheckSphere(transform.position, 3, layerMask).Equals(false))
            {
                state.ExitState();
                SetState(new PathingState(this));
                state.EnterState();
                attackableInRange = false;
            }
        }
        Debug.Log("Structure dead");

        //Restarts the coroutine upon entering pathing state again
        StartCoroutine(ProximityCheck());
        Debug.Log("Started Proximity Check again");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
