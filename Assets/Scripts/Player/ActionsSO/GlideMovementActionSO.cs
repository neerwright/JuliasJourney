using UnityEngine;
using Statemachine;

namespace Player
{

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
        [SerializeField] [Range(0.1f, 100f)] public float _acceleration = 12f;
        [SerializeField] [Range(0.1f, 100f)] public float _drop = 40f;
		
		[SerializeField] [Range(0.1f, 100f)] private float _airResistance = 20f;

		protected override StateAction CreateAction() => new GlideMovementAction();
	}

	public class GlideMovementAction : StateAction
	{
		private new GlideMovementActionSO OriginSO => (GlideMovementActionSO)base.OriginSO;

		private PlayerScript _player;
		private PlayerController pc;
        private float _speed;
        private float _acceleration;
        private bool _decellerating = false;
		private bool endGlide = false;
		private float maxSpeed;
		private float reduceMaxSpeed;

		

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			pc = stateMachine.GetComponent<PlayerController>();
            _speed = OriginSO._speed;
            _acceleration = OriginSO._acceleration ;
			maxSpeed = OriginSO.MaxSpeed;
			reduceMaxSpeed = maxSpeed /10;
		}

		public void ResetGlide()
		{
			_speed = OriginSO._speed;
            _acceleration = OriginSO._acceleration ;
			maxSpeed = OriginSO.MaxSpeed;
			reduceMaxSpeed = maxSpeed /10;
		}

		public override void OnUpdate()
		{
			if(_player.interactInput)
			{
				pc.IsGliding = false;
				endGlide = false;
				ResetGlide();
			}

			float airResistance = OriginSO.AirResistance;

			if (endGlide)
			{
				ApplyAirResistance(ref _player.movementVector.x, airResistance , 10);
				return;
			}

			Vector2 velocity = _player.movementVector;
			Vector2 input = _player.movementInput;
            float drop = OriginSO.Drop;

            
            if(_speed > maxSpeed)
            {
				maxSpeed = maxSpeed - reduceMaxSpeed;
				
				if(maxSpeed < reduceMaxSpeed * 5)
				{
					endGlide = true;
				}
                _speed = -drop;
                _player.movementVector.y = -10f;
            }
            

            SetVelocity(ref velocity.y, input.y, ref _speed, _acceleration);
				

			ApplyAirResistance(ref velocity.x, airResistance , _speed);
			_player.movementVector = velocity;
			
			
		}


		private void SetVelocity(ref float currentAxisSpeed, float axisInput, ref float speed, float acceleration)
		{
            
			if (axisInput > 0.1f)
			{
				currentAxisSpeed += axisInput * speed * Time.deltaTime;
                speed += Time.deltaTime * acceleration;
                
                
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
