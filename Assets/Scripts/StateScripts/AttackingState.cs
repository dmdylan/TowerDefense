using System.Collections;
using UnityEngine;

namespace StateStuff
{
    class AttackingState : State
    {
        public AttackingState(NPCBehavior npcBehavior) : base(npcBehavior)
        {
            EnterState();
        }

        public override void EnterState()
        {
        }

        public override IEnumerator UpdateState()
        {
            while (npcBehavior.attackableInRange.Equals(true)) 
            { 
                yield return new WaitForSeconds(.5f);
                Debug.Log("Attacking");
            }
        }

        public override void ExitState()
        {

        }
    }
}
