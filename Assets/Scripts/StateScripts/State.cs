using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public abstract void UpdateState();

        public virtual void ExitState()
        {

        }
    }
}
