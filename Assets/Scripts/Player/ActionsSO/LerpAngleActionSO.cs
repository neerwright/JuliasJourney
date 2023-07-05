using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "LerpRotationAction", menuName = "State Machine/Actions/Lerp Rotation")]
	public class LerpAngleActionSO : StateActionSO<LerpAngleAction>
	{
		public float lerpSpeed = 2f;
        public AnimationCurve curve;
        public Vector3 goalRotation;
        public string modelToLerpTag;
	}

	public class LerpAngleAction : StateAction
	{
		//Component references
        private AnimationCurve _curve;
		//private PlayerScript _playerScript;
        private GameObject _model;
        private Quaternion _startRotation;
		private LerpAngleActionSO _originSO => (LerpAngleActionSO)base.OriginSO; // The SO this StateAction spawned from
        private float _current = 0f;
        private float _target = 1f;

        private bool _finished = false;

		public override void Awake(StateMachine stateMachine)
		{
			//_playerScript = stateMachine.GetComponent<PlayerScript>();
            _model = GameObject.FindWithTag(_originSO.modelToLerpTag);
            _curve = _originSO.curve;
            _startRotation = _model.transform.rotation;       
		}

		public override void OnUpdate()
		{
            
            if(_finished)
                return;

            _current = Mathf.MoveTowards(_current, _target, _originSO.lerpSpeed * Time.deltaTime);
            if(_current >= 0.02f)
            {
                _current = 1;
                _finished = true;
            }

            _model.transform.rotation = Quaternion.Slerp(_model.transform.rotation, Quaternion.Euler(_originSO.goalRotation), _curve.Evaluate(_current));
		}

        public override void OnStateEnter()
		{
            _finished = false;
            _current = 0;
        }
	}
}