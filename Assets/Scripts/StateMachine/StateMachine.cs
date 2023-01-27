using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Tooltip("Set the initial state of this StateMachine")]
	[SerializeField] private TransitionTableSO _transitionTableSO;
    
    internal State _currentState;
    private float _stateTimeElapsed = 0;

    void Awake()
    {
		_currentState = _transitionTableSO.GetInitialState(this);
    }
    
    private void Start()
	{
		_currentState.OnStateEnter();
	}
    
    private void Update()
	{
		if (_currentState.TryGetTransition(out var transitionState))
			Transition(transitionState);

		_currentState.OnUpdate();
	}

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }    

	private void Transition(State transitionState)
	{
		_currentState.OnStateExit();
		_currentState = transitionState;
		_currentState.OnStateEnter();

        _stateTimeElapsed = 0;

		}
    
    
    
    //----------------------------



    public void SetState(State state)
    {
        _currentState = state;
 		_currentState.OnStateEnter();       
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        _stateTimeElapsed += Time.deltaTime;
        return(_stateTimeElapsed >= duration);
    }

    public void ResetStateTimer()
    {
        _stateTimeElapsed = 0;
    }

    //Debug-----------------------
    private void OnDrawGizmos() 
    {
        if (_currentState != null )
        {
            Gizmos.color = _currentState._sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 3f);
        }
    }
  
}
