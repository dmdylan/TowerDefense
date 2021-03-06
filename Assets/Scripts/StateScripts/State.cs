﻿using System;
using System.Collections;
using UnityEngine;

namespace StateStuff
{
    public abstract class State
    {
        protected Enemy enemy;
        
        public State(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public abstract void EnterState();

        public abstract IEnumerator UpdateState();

        public abstract void ExitState();
    }
}
