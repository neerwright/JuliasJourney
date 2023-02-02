using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(menuName = "State Machine/Conditions/TimerCondition")]
	public class TimeElapsedConditionSO : StateConditionSO<TimeElapsedCondition>
	{
		[TextArea] public string description;
		
		public float timerLength = .5f;
	}

	public class TimeElapsedCondition : Condition
	{
		private float _startTime;
		private TimeElapsedConditionSO _originSO => (TimeElapsedConditionSO)base.OriginSO; // The SO this Condition spawned from

		public override void OnStateEnter()
		{
			_startTime = Time.time;
		}

		protected override bool Statement() => Time.time >= _startTime + _originSO.timerLength;
		
	}
}