using System.Collections;
using UnityEngine;

namespace StateStuff
{
    class AttackingState : State
    {
        public AttackingState(NPCBehavior npcBehavior) : base(npcBehavior)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override IEnumerator UpdateState()
        {
            while (true) 
            { 
                Debug.Log("Attacking");
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
