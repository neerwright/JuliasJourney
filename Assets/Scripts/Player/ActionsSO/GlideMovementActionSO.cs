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
        private float _speed;
        private float _acceleration;
        private bool _decellerating = false;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
            _speed = OriginSO._speed;
            _acceleration = OriginSO._acceleration ;
		}

		public override void OnUpdate()
		{
			Vector2 velocity = _player.movementVector;
			Vector2 input = _player.movementInput;
			float maxSpeed = OriginSO.MaxSpeed;
            float drop = OriginSO.Drop;

			float airResistance = OriginSO.AirResistance;
            
            if(_speed > maxSpeed)
            {
                Debug.Log(_speed);
                _speed = -drop;
                _player.movementVector.y = -10f;
            }
            

            SetVelocity(ref velocity.y, input.y, ref _speed, _acceleration);
				

			ApplyAirResistance(ref velocity.x, airResistance );
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

		private void ApplyAirResistance(ref float value, float airResistance)
		{
			float sign = Mathf.Sign(value);

			value -= sign * airResistance * Time.deltaTime;
			if (Mathf.Sign(value) != sign)
				value = 0;
		}
	}
}
