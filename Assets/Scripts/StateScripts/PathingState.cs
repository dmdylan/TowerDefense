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

        public override void UpdateState()
        {
            Debug.Log("Pathing");
        }
    }
}
