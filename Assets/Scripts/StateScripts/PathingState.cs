using System.Collections;
using UnityEngine;

namespace StateStuff
{
    class PathingState : State
    {
        public PathingState(Enemy enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            enemy.navMeshAgent.enabled = true;

            if (enemy.navMeshAgent.Equals(null))           
                return;         
            else if (enemy.navMeshAgent.isStopped)          
                enemy.navMeshAgent.isStopped = false;           
      
            else          
                enemy.navMeshAgent.SetDestination(enemy.objective.position);           
        }

        public override IEnumerator UpdateState()
        {
            while (enemy.attackableInRange.Equals(false))
            {
                yield return new WaitForSeconds(.25f);               
            }
        }

        public override void ExitState()
        {
            enemy.navMeshAgent.isStopped = true;
        }
    }
}
