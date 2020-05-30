using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using StateStuff;
using System.Linq;

public class Enemy : NPCStateMachine, IPoolObject, IDamageable
{
    public BaseEnemyStats baseEnemyStats;
    public NavMeshAgent navMeshAgent;
    public bool attackableInRange = false;
    public readonly int layerMask = 1 << 8;
    private State currentState;
    public Vector3 Objective { get; set; }
    public Spawner spawner;

    private float currentHealth;

    private void OnEnable()
    {
       navMeshAgent = GetComponent<NavMeshAgent>();
       navMeshAgent.enabled = false;
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
        EnemySetup();
        SetState(new PathingState(this));
        currentState = state;
        StartCoroutine(ProximityCheck());
        StartCoroutine(state.UpdateState());
    }

    void EnemySetup()
    {
        currentHealth = baseEnemyStats.MaxHealth;
    }

    //TODO: Set up game event for when enemy dies
    public void Destroy() => gameObject.SetActive(false);

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth - damageAmount <= 0)
            Destroy();
        else
            currentHealth -= damageAmount;
    }
}
