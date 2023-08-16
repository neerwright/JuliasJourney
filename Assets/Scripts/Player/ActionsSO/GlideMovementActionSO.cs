using UnityEngine;
using Statemachine;

namespace Player
{
//TODO REFACTOR THIS SHIT
	/// <summary>
	/// This Action handles horizontal movement while in the air, keeping momentum, simulating air resistance, and accelerating towards the desired speed.
	/// </summary>
	[CreateAssetMenu(fileName = "GlideMovement", menuName = "State Machine/Actions/Glide Movement")]
	public class GlideMovementActionSO : StateActionSO
	{
		public float MaxSpeed => _maxSpeed;
		public float AirResistance => _airResistance;
        public float Drop => _drop;
        

		

		
		[SerializeField] [Range(0.1f, 100f)] private float _maxSpeed = 12f;
        [SerializeField] [Range(0.1f, 100f)] public float _speed = 12f;
		[SerializeField] [Range(0.1f, 100f)] public float rotationSpeed = 12f;
        [SerializeField] [Range(0.1f, 100f)] public float _acceleration = 12f;
        [SerializeField] [Range(0.1f, 100f)] public float _drop = 40f;
		public AnimationCurve curve;
		
		[SerializeField] [Range(0.1f, 100f)] private float _airResistance = 20f;

		protected override StateAction CreateAction() => new GlideMovementAction();
	}

	public class GlideMovementAction : StateAction
	{
		private new GlideMovementActionSO OriginSO => (GlideMovementActionSO)base.OriginSO;

		private PlayerScript _playerScript;
		private PlayerController pc;
		private GameObject _playerModel;

        private float _speed;
        private float _acceleration;
		private bool endGlide = false;
		private float maxSpeed;
		private float reduceMaxSpeed;

		private float _maxUpAngle = 50f;
		private const float DROP_ANGLE = 110;

		private bool _dropped = false;
		private bool _finishedDropped = true;
		private float _current = 0f;
        private float _target = 1f;
		private float _rotationAdjustment = 0f;
		private AnimationCurve _curve;

		private bool unlock = false;

		public override void Awake(StateMachine stateMachine)
		{
			_playerScript = stateMachine.GetComponent<PlayerScript>();
			pc = stateMachine.GetComponent<PlayerController>();
			_playerModel = GameObject.FindWithTag("PlayerModel");
			
            _speed = OriginSO._speed;
            _acceleration = OriginSO._acceleration ;
			maxSpeed = OriginSO.MaxSpeed;
			reduceMaxSpeed = maxSpeed /10;

			_curve = OriginSO.curve;
		}

		public void ResetGlide()
		{
			pc.IsGliding = false;
			endGlide = false;
			_dropped = false;

			_speed = OriginSO._speed;
            _acceleration = OriginSO._acceleration ;
			maxSpeed = OriginSO.MaxSpeed;
			reduceMaxSpeed = maxSpeed /10;
		}

		public override void OnUpdate()
		{
			Quaternion currRotation = _playerModel.transform.rotation;
			Vector3 currentEulerAngles = currRotation.eulerAngles;

			if(currentEulerAngles.x > 70)
				unlock = true;

			if (!unlock)
				return;	

			float airResistance = OriginSO.AirResistance;

			if (endGlide)
			{
				ApplyAirResistance(ref _playerScript.movementVector.x, airResistance , 10);
				return;
			}

			if(_dropped && !_finishedDropped)
			{
				
				//DropRotatePlayer(DROP_ANGLE - _rotationAdjustment);
			}
				

			Vector2 velocity = _playerScript.movementVector;
			Vector2 input = _playerScript.movementInput;
            float drop = OriginSO.Drop;

            
            if(_speed > maxSpeed)
            {
				maxSpeed = maxSpeed - reduceMaxSpeed;
				
				if(maxSpeed < reduceMaxSpeed * 5)
				{
					endGlide = true;
				}
                //_speed = -drop;
                //_playerScript.movementVector.y = -10f;
				_dropped = true;
				_finishedDropped = false;
				_rotationAdjustment += 5;
            }


            SetVelocity(ref velocity.y, input.y, ref _speed, _acceleration);
				

			ApplyAirResistance(ref velocity.x, airResistance , _speed);
			_playerScript.movementVector = velocity;
			
			
		}

		private void DropRotatePlayer(float angle)
		{
			

		}

		private void RotatePlayer()
		{
			if(!_dropped)
			{

			}
			else
			{
				DropRotatePlayer(DROP_ANGLE - 40 + _rotationAdjustment);
			}
			
		}

		private void SetVelocity(ref float currentAxisSpeed, float axisInput, ref float speed, float acceleration)
		{
            
			if (axisInput > 0.1f)
			{
				currentAxisSpeed += axisInput * speed * Time.deltaTime;
                speed += Time.deltaTime * acceleration;
                
				if(_finishedDropped)
					RotatePlayer();
				
                
			}
            else
            {
                //speed = Mathf.Max(speed, OriginSO._speed);
            }
            
		}

		private void ApplyAirResistance(ref float value, float airResistance, float speed)
		{
			
				float sign = Mathf.Sign(value);
				float speedSign = Mathf.Sign(speed);
				if(speedSign == 1)
					speed = Mathf.Max(1, speed);
				if(speedSign == -1)
					speed = Mathf.Min(-1, speed);

				value -= sign * airResistance * Time.deltaTime * Mathf.Abs(speed) / 100f;
				if (Mathf.Sign(value) != sign)
					value = 0;
			
			
		}
	}
}
