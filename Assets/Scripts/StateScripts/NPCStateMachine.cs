using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class NPCStateMachine : MonoBehaviour
    {
        protected State state;

        public void SetState(State state)
        {
            this.state = state;
            state.EnterState();
        }
    }
}
