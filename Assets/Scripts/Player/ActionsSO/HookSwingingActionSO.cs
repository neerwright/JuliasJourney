using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "HookSwingAction", menuName = "State Machine/Actions/Hook Swinging")]
	public class HookSwingingActionSO : StateActionSO
	{
        public GameObjectVariableSO HookTarget;
		//public float _deAcceleration = 1f;

		protected override StateAction CreateAction() => new HookSwingingAction();
	}

	public class HookSwingingAction : StateAction
	{
		private Player _player;
        private Vector2 _targetPosition;
        private Vector2 _ropePosition;
        private float _swingAngleVelocity = 0;
        private float _ropeAngle;
        private float _ropeLength;

		private new HookSwingingActionSO OriginSO => (HookSwingingActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
		}

		public override void OnUpdate()
		{
			//_player.movementVector.y = 0;
			//_player.movementVector.x = Mathf.MoveTowards(_player.movementVector.x, 0, OriginSO._deAcceleration * Time.deltaTime);
		
            float ropeAngleAcceleration = (float) -0.5 * Mathf.Cos(_ropeAngle * Mathf.Deg2Rad) * Time.deltaTime ;
            Debug.Log(ropeAngleAcceleration);
            _swingAngleVelocity += ropeAngleAcceleration;
            _ropeAngle += _swingAngleVelocity;
            //_swingAngleVelocity *= 0.99;
            _ropePosition.x = _targetPosition.x + _ropeLength * (Mathf.Cos(_ropeAngle * Mathf.Deg2Rad)); 
            _ropePosition.y = _targetPosition.y + _ropeLength * (Mathf.Sin(_ropeAngle * Mathf.Deg2Rad)); 

            Debug.DrawRay( _player.transform.position, _ropePosition - (Vector2)_player.transform.position , Color.yellow, 1f);
            _player.movementVector.x = (_ropePosition.x - _player.transform.position.x) * 10;
            _player.movementVector.y = (_ropePosition.y - _player.transform.position.y) * 10;

        }


		public override void OnStateEnter()
		{
            _swingAngleVelocity = 0;
            _targetPosition = OriginSO.HookTarget.GameObject.transform.position;
            _ropePosition = _player.transform.position;
            Vector2 dir = _targetPosition - _ropePosition;
            Debug.DrawRay(_ropePosition, dir , Color.red, 4f);
            _ropeAngle = Vector2.SignedAngle(Vector2.right , -dir);
            if (_ropeAngle < 0)
                _ropeAngle = 360 + _ropeAngle;

            Debug.Log(_ropeAngle);
            _ropeLength = dir.magnitude;
		}

		public override void OnStateExit()
		{

		}
	}
}