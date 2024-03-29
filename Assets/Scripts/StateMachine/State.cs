using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statemachine
{

	public class State
	{
		internal StateSO _originSO;
		internal StateMachine _stateMachine;
		internal StateTransition[] _transitions;
		internal StateAction[] _actions;
		
		internal Color _sceneGizmoColor;


		internal State() { }

		public State(
			StateSO originSO,
			StateMachine stateMachine,
			StateTransition[] transitions,
			StateAction[] actions,
			Color sceneGizmoColor
			)
		{
			_originSO = originSO;
			_stateMachine = stateMachine;
			_transitions = transitions;
			_actions = actions;
			_sceneGizmoColor = sceneGizmoColor;
		}
		//'------------------------
		public void OnStateEnter()
		{
			void OnStateEnter(IStateComponent[] comps)
			{
					for (int i = 0; i < comps.Length; i++)
						comps[i].OnStateEnter();
			}
			OnStateEnter(_transitions);
			OnStateEnter(_actions);
		}

			public void OnUpdate()
			{
				for (int i = 0; i < _actions.Length; i++)
					_actions[i].OnUpdate();
			}

			public void OnFixedUpdate()
			{
				for (int i = 0; i < _actions.Length; i++)
					_actions[i].OnFixedUpdate();
			}

			public void OnStateExit()
			{
				void OnStateExit(IStateComponent[] comps)
				{
					for (int i = 0; i < comps.Length; i++)
						comps[i].OnStateExit();
				}
				OnStateExit(_transitions);
				OnStateExit(_actions);
			}
			
		//----Transitions--------called from state machine-----delegated to transition--
		public bool TryGetTransition(out State state)
			{
				state = null;

				for (int i = 0; i < _transitions.Length; i++)
					if (_transitions[i].TryGetTransiton(out state))
						break;

				//caching for later
				//for (int i = 0; i < _transitions.Length; i++)
				//	_transitions[i].ClearConditionsCache();

				return state != null;
			}    

	}
}