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
    bool switchStates = false; //for debugging
    bool attackableInRange = false;
    int layerMask = 1 << 8;

    
    //TODO: Setup way in code for object to be auto assigned to enemy through code
    //TODO: Check for obstacles in range to attack and switch state. Range check? Physics.Overlapsphere/Checksphere?

    // Start is called before the first frame update
    void Start()
    {
       navMeshAgent = this.GetComponent<NavMeshAgent>();
       SetState(new PathingState(this));
       state.EnterState(); 
       StartCoroutine(ProximityCheck());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (switchStates.Equals(false))
            {
                SetState(new AttackingState(this));
                switchStates = true;
            }
            else
            {
                SetState(new PathingState(this));
                switchStates = false;
            }
        }

        state.UpdateState();
    }

    IEnumerator ProximityCheck()
    {
        while (Physics.CheckSphere(transform.position, 3, layerMask).Equals(false))
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Checking for enemies");
            //if (Physics.CheckSphere(transform.position, 3))
              //  break;
        }

        Debug.Log("Found enemy");
        StopCoroutine(ProximityCheck());
        //yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
