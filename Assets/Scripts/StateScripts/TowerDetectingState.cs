using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace StateStuff
{
    public class TowerDetectingState : TowerState
    {
        public TowerDetectingState(Structure structure) : base(structure)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override IEnumerator UpdateState()
        {
            yield return null;
        }
    }
}
