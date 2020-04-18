using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class NPCBehavior : NPCStateMachine
{
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
       currentState = SetState(new PathingState(this));      
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
}
