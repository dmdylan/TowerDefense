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
            Debug.Log("TowerDetectingState - EnterState()");
        }

        public override void ExitState()
        {
            Debug.Log("TowerDetectingState - ExitState()");
        }

        public override IEnumerator UpdateState()
        {
            Debug.Log("TowerDetectingState - UpdateState()");
            yield return null;
        }
    }
}
