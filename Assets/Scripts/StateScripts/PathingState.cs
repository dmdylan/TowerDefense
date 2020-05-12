using System.Collections;
using UnityEngine;

namespace StateStuff
{
    class PathingState : State
    {
        public PathingState(NPCBehavior npcBehavior) : base(npcBehavior)
        {
        }

        public override void EnterState()
        {
            npcBehavior.navMeshAgent.enabled = true;

            if (npcBehavior.navMeshAgent.Equals(null))           
                return;         
            else if (npcBehavior.navMeshAgent.isStopped)          
                npcBehavior.navMeshAgent.isStopped = false;           
      
            else          
                npcBehavior.navMeshAgent.SetDestination(npcBehavior.objective.position);           
        }

        public override IEnumerator UpdateState()
        {
            while (npcBehavior.attackableInRange.Equals(false))
            {
                yield return new WaitForSeconds(.25f);               
            }
        }

        public override void ExitState()
        {
            npcBehavior.navMeshAgent.isStopped = true;
        }
    }
}
