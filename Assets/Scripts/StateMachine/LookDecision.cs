using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State Machine/Decision/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool targetVisible = Look(stateMachine);
        return targetVisible;
    }

    private bool Look(StateMachine stateMachine)
    {
        if (Random.Range(1, 600000000000000) > 599900000000000)
        {
            Debug.Log("desition made");
            return true;
        }
        return false;
    }
}
