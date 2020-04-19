using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class NPCBehavior : NPCStateMachine
{
    public BaseEnemyStats baseEnemyStats;
    bool switchStates = false; //for debugging

    // Start is called before the first frame update
    void Start()
    {
       SetState(new PathingState(this));      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (switchStates.Equals(false))
            {
                SetState(new AttackingState(this));
                switchStates = true;
            }
            else
            {
                SetState(new PathingState(this));
                switchStates = false;
            }
        }

        state.UpdateState();
    }
}
