using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Statemachine;

namespace Player
{

    [CreateAssetMenu(menuName = "State Machine/Conditions/RandomCondition")]
    public class RandomConditionSO : StateConditionSO<RandomCondition>
    {
        public int threshold = 50;
    }

    public class RandomCondition : Condition
    {
        private StateMachine _stateMachine;
        private int timerDuration = 3;

        private RandomConditionSO _originSO => (RandomConditionSO)base.OriginSO; // The SO this Condition spawned from

        public override void Awake(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected override bool Statement()
        {
            
            bool targetVisible = false;
            bool timerUp = _stateMachine.CheckIfCountDownElapsed(timerDuration);
            if (timerUp)
            {
                targetVisible = Decide(_originSO.threshold );
                _stateMachine.ResetStateTimer();
            }
            return targetVisible;
        }

        private bool Decide(int i)
        {
            if (Random.Range(1, 100) > i)
            {
                return true;
            }
            return false;
        }
    }
}