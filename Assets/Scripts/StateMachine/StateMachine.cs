using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [Tooltip("Set the initial state of this StateMachine")]
	[SerializeField] private ScriptableObjects.TransitionTableSO _transitionTableSO;
    
    internal State _currentState;
    private float stateTimeElapsed = 0;

    void Awake()
    {
		_currentState = _transitionTableSO.GetInitialState(this);
    }
    //----------------------------

        void Update()
    {
        currentState.UpdateState(this);
    }

    public void SetState(State state)
    {
        currentState = state;
        //TODO: start Enter Phase of the state
    }

    //Decision --> transition to new state? null -> remain in same state
    public void TransitionToState(State nextState)
    {
        if (nextState != null)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return(stateTimeElapsed >= duration);
    }

    protected void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    //Debug
    private void OnDrawGizmos() 
    {
        if (currentState != null )
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 3f);
        }
    }
  
}
