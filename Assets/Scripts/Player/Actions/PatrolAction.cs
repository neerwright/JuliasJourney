using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "State Machine/Actions/Patrol")]
public class PatrolAction : Action
{
    private Player playerScript;
    
    public override  void OnStateEnter(StateMachine stateMachine)
    {
        playerScript = stateMachine.GetComponent<Player>();
    }

    public override void Act(StateMachine stateMachine)
    {
        Patrol(stateMachine);
    }

    private void Patrol(StateMachine stateMachine)
    {
        Debug.Log("Patrolling...");
    }

}
