using UnityEngine;
using Statemachine;

namespace Player
{

	/// <summary>
	/// This Action handles horizontal movement while in the air, keeping momentum, simulating air resistance, and accelerating towards the desired speed.
	/// </summary>
	[CreateAssetMenu(fileName = "AerialMovement", menuName = "State Machine/Actions/Aerial Movement")]
	public class AerialMovementActionSO : StateActionSO
	{
		public float MaxSpeedInAir => _maxAirSpeed;
		public float AbsoluteMaxSpeed => _absoluteMaxSpeed;
		public float Acceleration => _acceleration;
		public float AirResistance => _airResistance;

		public float ApexBonus => _apexBonus;
		public float JumpApexThreshold => _jumpApexThreshold;

		[Tooltip("Desired horizontal movement speed while in the air")]
		[SerializeField] [Range(0.1f, 100f)] private float _maxAirSpeed = 12f;
		[Tooltip("Desired horizontal movement speed while in the air")]
		[SerializeField] [Range(0.1f, 100f)] private float _absoluteMaxSpeed = 30f;
		[Tooltip("The acceleration applied to reach the desired speed")]
		[SerializeField] [Range(0.1f, 100f)] private float _acceleration = 20f;
		[Tooltip("air Resistance applied at the end to dampen movement")]
		[SerializeField] [Range(0.1f, 100f)] private float _airResistance = 20f;

		[Tooltip("extra speed during apex of jump")]
		[SerializeField] private float _apexBonus = 2;
		[Tooltip("When upwards velocity is smaller than threshold, the apex bonus gets applied gradually")]
		[SerializeField] private float _jumpApexThreshold = 10f;
		protected override StateAction CreateAction() => new AerialMovementAction();
	}

	public class AerialMovementAction : StateAction
	{
		private new AerialMovementActionSO OriginSO => (AerialMovementActionSO)base.OriginSO;

		private Player _player;
		private float _apexPoint = 0f;
		private float _jumpApexThreshold;
		private float _apexBonus = 2f;
		private bool _applyExtraAirResistance = false;
		private const float MAX_SPEED_THREASHOLD = 12.5f;



		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
			
		}

		public override void OnUpdate()
		{
			Vector2 velocity = _player.movementVector;
			Vector2 input = _player.movementInput;
			float maxSpeedInAir = OriginSO.MaxSpeedInAir;
			float absMaxSpeed = OriginSO.AbsoluteMaxSpeed;
			float airResistance = OriginSO.AirResistance;

			float acceleration = OriginSO.Acceleration / 100f;
			_jumpApexThreshold = OriginSO.JumpApexThreshold;
			_apexBonus = OriginSO.ApexBonus;

			float apexBonus = CalculateApexBonus(velocity.y, input.x);
			Debug.Log(apexBonus);
			
			
			//

			if (Mathf.Abs(velocity.x) <= maxSpeedInAir)
			{
				SetVelocity(ref velocity.x, input.x, acceleration);
				velocity.x += apexBonus;
				velocity.x = Mathf.Clamp(velocity.x, -maxSpeedInAir , maxSpeedInAir);
				
				
			}
			else
			{	
				if(Mathf.Sign(input.x) != Mathf.Sign(velocity.x))
				{
					SetVelocity(ref velocity.x, input.x, acceleration);
				}
				//Absolute MaxSpeed
				velocity.x = Mathf.Clamp(velocity.x, -absMaxSpeed, absMaxSpeed);
			}
			
			
			ApplyAirResistance(ref velocity.x, airResistance );
			_player.movementVector = velocity;
			
			
		}

		private float CalculateApexBonus(float velocity, float horizontalInput)
		{
			_applyExtraAirResistance = false;
			_apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(velocity));
			var apexBonus = Mathf.Sign(horizontalInput) * _apexBonus * _apexPoint;
			
			apexBonus =(Mathf.Abs(horizontalInput) > Mathf.Epsilon)? apexBonus * Time.deltaTime : 0f;

			return apexBonus;
		}

		private void SetVelocity(ref float currentAxisSpeed, float axisInput, float acceleration)
		{
			if (axisInput != 0f)
			{
				currentAxisSpeed += axisInput * acceleration;
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
