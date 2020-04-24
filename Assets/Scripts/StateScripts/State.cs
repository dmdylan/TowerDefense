using System;
using System.Collections;
using UnityEngine;

namespace StateStuff
{
    public abstract class State
    {
        protected NPCBehavior npcBehavior;
        
        public State(NPCBehavior npcBehavior)
        {
            this.npcBehavior = npcBehavior;
        }

        public virtual void EnterState()
        {
            
        }

        public abstract IEnumerator UpdateState();

        public abstract void ExitState();
    }
}
