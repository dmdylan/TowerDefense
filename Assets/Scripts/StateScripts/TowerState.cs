using System;
using System.Collections;

namespace StateStuff
{
    public abstract class TowerState
    {
        protected Structure structure;

        public TowerState(Structure structure)
        {
            this.structure = structure;
        }

        public abstract void EnterState();
        public abstract IEnumerator UpdateState();
        public abstract void ExitState();
    }
}