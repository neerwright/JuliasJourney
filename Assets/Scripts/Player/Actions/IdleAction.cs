using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "State Machine/Actions/Idle")]

public class IdleAction : Action
{
    private Player playerScript;
    
    public override  void OnStateEnter(StateMachine stateMachine)
    {
        playerScript = stateMachine.GetComponent<Player>();
    }

    public override void Act(StateMachine stateMachine)
    {
        Idle(stateMachine);
    }

    private void Idle(StateMachine stateMachine)
    {
        Debug.Log("Idling...");
    }
}
