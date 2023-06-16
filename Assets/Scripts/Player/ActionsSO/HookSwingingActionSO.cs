using UnityEngine;
using Statemachine;
using Scriptables;
using System.Linq;

namespace Player
{

	[CreateAssetMenu(fileName = "HookSwingAction", menuName = "State Machine/Actions/Hook Swinging")]
	public class HookSwingingActionSO : StateActionSO
	{
        public GameObjectSet HookTargets;
        public float SwingSpeed = 10f;
		public float InitialVelocity = 10f;

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
			
            float ropeAngleAcceleration = (float) -0.5 * Mathf.Cos(_ropeAngle * Mathf.Deg2Rad) * Time.deltaTime ;
            _swingAngleVelocity += ropeAngleAcceleration;
            _swingAngleVelocity += Time.deltaTime * _player.movementInput.x * 0.1f;
            _ropeAngle += _swingAngleVelocity;
            //_swingAngleVelocity *= 0.99;
            _ropePosition.x = _targetPosition.x + _ropeLength * (Mathf.Cos(_ropeAngle * Mathf.Deg2Rad)); 
            _ropePosition.y = _targetPosition.y + _ropeLength * (Mathf.Sin(_ropeAngle * Mathf.Deg2Rad)); 

            _player.movementVector.x = (_ropePosition.x - _player.transform.position.x) * OriginSO.SwingSpeed;
            _player.movementVector.y = (_ropePosition.y - _player.transform.position.y) * OriginSO.SwingSpeed;

        }


		public override void OnStateEnter()
		{
            GameObject HookCenter;
            GameObject HookTarget;
            HookTarget = OriginSO.HookTargets.Items.Where(p => p.tag == "Hook").OrderBy(p => Vector2.Distance(p.transform.position, _player.transform.position)).FirstOrDefault();
            HookCenter = HookTarget.transform.parent.gameObject;

            _targetPosition = HookCenter.transform.position;
            _ropePosition = _player.transform.position;
            Vector2 dir = _targetPosition - _ropePosition;
            _ropeAngle = Vector2.SignedAngle(Vector2.right , -dir);
            if (_ropeAngle < 0)
                _ropeAngle = 360 + _ropeAngle;

            _ropeLength = dir.magnitude;

            CalculateInitialVelocity(dir);
		}

		public override void OnStateExit()
		{

		}

        private void CalculateInitialVelocity(Vector2 dir)
        {
            _swingAngleVelocity = 0;
            float initialVelocity = 0;
            
            // if we started the hook on the side we are not moving towards -> more initial speed
            float multiplyer = -(Mathf.Sign(_player.movementVector.x)) * (Mathf.Cos(_ropeAngle * Mathf.Deg2Rad) - Mathf.Sign(_player.movementVector.x));
            multiplyer = Mathf.Clamp(multiplyer, -1f, 1f);
            
            initialVelocity = _player.movementVector.x * Time.deltaTime * OriginSO.InitialVelocity * multiplyer;

            _swingAngleVelocity = initialVelocity;
                
        }
	}
}