using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "State Machine/State")]
public class State : ScriptableObject
{
    public List<Transition> transitions = new List<Transition>();
    public Color sceneGizmoColor = Color.grey;
    public List<Action> actions = new List<Action>();
    //Invoked from controller, do actions, evaluate decisions
    public void UpdateState(StateMachine stateMachine)
    {
        DoActions(stateMachine);
        CheckTransitions(stateMachine);
    }

    private void DoActions(StateMachine stateMachine)
    {
        foreach (Action action in actions)
        {
            action.Act(stateMachine);
        }
    }

    private void CheckTransitions(StateMachine stateMachine)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(stateMachine);
            if (decisionSucceeded)
            {
                stateMachine.TransitionToState(transition.trueState);
            }
            else
            {
                stateMachine.TransitionToState(transition.falseState);
            }
        }
    }
}
