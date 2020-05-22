using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class TowerAttackingState : TowerState
    {
        public TowerAttackingState(Structure structure) : base(structure)
        {
        }

        public override void EnterState()
        {
            Debug.Log("TowerAttackingState - EnterState()");
        }

        public override void ExitState()
        {
            Debug.Log("TowerAttackingState - ExitState()");
        }

        public override IEnumerator UpdateState()
        {
            Debug.Log("TowerAttackingState - UpdateState()");
            yield return null;
        }
    }
}