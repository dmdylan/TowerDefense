using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using StateStuff;
using System.Linq;

public class NPCBehavior : NPCStateMachine, IPoolObject
{
    public Transform objective = null;
    public BaseEnemyStats baseEnemyStats;
    public NavMeshAgent navMeshAgent;
    public bool attackableInRange = false;
    public readonly int layerMask = 1 << 8;
    private State currentState;
    
    //TODO: Setup way in code for object to be auto assigned to enemy through code

    // Start is called before the first frame update
    void Start()
    { 
       navMeshAgent = GetComponent<NavMeshAgent>();
       SetState(new PathingState(this));
       currentState = state;
       StartCoroutine(ProximityCheck());
       StartCoroutine(state.UpdateState());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != state)
        {
            StartCoroutine(state.UpdateState());
            currentState = state;
        }
    }

    IEnumerator ProximityCheck()
    {
        //while there are no objects to attack in attack range
        while (attackableInRange.Equals(false))
        {
            yield return new WaitForSeconds(.25f);
            //Checks for any structures in attack range, if there are then it 
            //changes to attack state and sets bool to true
            if(Physics.CheckSphere(transform.position, baseEnemyStats.AttackRange, layerMask))
            {

                state.ExitState();
                SetState(new AttackingState(this));
                attackableInRange = true;
            }
        }

        //while there is an object in range to attack
        while (attackableInRange.Equals(true))
        {
            yield return new WaitForSeconds(.25f);

            //checks if there are no structures in range to attack (the structure was destroyed)
            //if so it changes back to pathing state and sets bool to false
            if (Physics.CheckSphere(transform.position, baseEnemyStats.AttackRange, layerMask).Equals(false))
            {
                state.ExitState();
                SetState(new PathingState(this));
                attackableInRange = false;
            }
        }

        //Restarts the coroutine upon entering pathing state again
        StartCoroutine(ProximityCheck());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, baseEnemyStats.AttackRange);
    }

    public void OnObjectReuse()
    {
        SetState(new PathingState(this));
        currentState = state;
        StartCoroutine(ProximityCheck());
        StartCoroutine(state.UpdateState());
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }
}
