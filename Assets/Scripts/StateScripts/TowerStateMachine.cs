using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateStuff
{
    class TowerStateMachine : MonoBehaviour
    {
        protected TowerState towerState;

        public void SetState(TowerState towerState)
        {
            this.towerState = towerState;
            towerState.EnterState();
        }
    }
}
