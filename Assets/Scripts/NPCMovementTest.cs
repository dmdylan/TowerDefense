using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementTest : MonoBehaviour
{
    [SerializeField] private Transform objective;
    [SerializeField] private float minimumPointDistance = .1f;
    [SerializeField] private Transform[] wayPoints = null;
    NavMeshAgent navMeshAgent;

    private int wayPointCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent.Equals(null))
            return;
        else
            navMeshAgent.SetDestination(objective.position);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(navMeshAgent.hasPath);
       // Debug.Log(navMeshAgent.pathStatus);
       // if (navMeshAgent.hasPath.Equals(false))
       //     navMeshAgent.isStopped = true;
       //
       // Debug.Log(navMeshAgent.path.corners);
    }

    private void SetDestination()
    {
        if (navMeshAgent.hasPath.Equals(false))
        {
            navMeshAgent.SetDestination(wayPoints[wayPointCounter].position);
            wayPointCounter++;
        }
        
        if (wayPointCounter >= wayPoints.Length && navMeshAgent.remainingDistance <= minimumPointDistance)
            Destroy(this.gameObject);
        else if(navMeshAgent.remainingDistance <= minimumPointDistance)
        {
            navMeshAgent.SetDestination(wayPoints[wayPointCounter].position);
            wayPointCounter++;
        }
    }

}
