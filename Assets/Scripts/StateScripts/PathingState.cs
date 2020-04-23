using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (npcBehavior.navMeshAgent.Equals(null))
                return;
            else if (npcBehavior.navMeshAgent.isStopped)          
                npcBehavior.navMeshAgent.isStopped = false;           
            else
                npcBehavior.navMeshAgent.SetDestination(npcBehavior.objective.position);
        }

        public override void UpdateState()
        {
            Debug.Log("Pathing");
        }

        public override void ExitState()
        {
            npcBehavior.navMeshAgent.isStopped = true;
        }
    }
}
