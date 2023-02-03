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
		public float Speed => _speed;
		public float Acceleration => _acceleration;
		public float ApexBonus => _apexBonus;
		public float JumpApexThreshold => _jumpApexThreshold;

		[Tooltip("Desired horizontal movement speed while in the air")]
		[SerializeField] [Range(0.1f, 100f)] private float _speed = 10f;
		[Tooltip("The acceleration applied to reach the desired speed")]
		[SerializeField] [Range(0.1f, 100f)] private float _acceleration = 20f;
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



		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
			
		}

		public override void OnUpdate()
		{
			Vector2 velocity = _player.movementVector;
			Vector2 input = _player.movementInput;
			float speed = OriginSO.Speed;
			float acceleration = OriginSO.Acceleration;
			_jumpApexThreshold = OriginSO.JumpApexThreshold;
			_apexBonus = OriginSO.ApexBonus;

			SetVelocity(ref velocity.x, input.x, acceleration, speed);
			
			_player.movementVector = velocity;
			_player.movementVector.x += CalculateApexBonus(velocity.y, input.x);
		}

		private float CalculateApexBonus(float velocity, float horizontalInput)
		{
			_apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(velocity));
			
			var apexBonus = Mathf.Sign(horizontalInput) * _apexBonus * _apexPoint;
			
			apexBonus =(Mathf.Abs(horizontalInput) > Mathf.Epsilon)? apexBonus * Time.deltaTime : 0f;
			return apexBonus;
			

		}

		private void SetVelocity(ref float currentAxisSpeed, float axisInput, float acceleration, float targetSpeed)
		{
			if (axisInput == 0f)
			{
				if (currentAxisSpeed != 0f)
				{
					ApplyAirResistance(ref currentAxisSpeed);
				}
			}
			else
			{
				(float absVel, float absInput) = (Mathf.Abs(currentAxisSpeed), Mathf.Abs(axisInput));
				(float signVel, float signInput) = (Mathf.Sign(currentAxisSpeed), Mathf.Sign(axisInput));
				targetSpeed *= absInput;

				if (signVel != signInput || absVel < targetSpeed)
				{
					currentAxisSpeed += axisInput * acceleration;
					currentAxisSpeed = Mathf.Clamp(currentAxisSpeed, -targetSpeed, targetSpeed);
				}
				else
				{
					ApplyAirResistance(ref currentAxisSpeed);
				}
			}
		}

		private void ApplyAirResistance(ref float value)
		{
			float sign = Mathf.Sign(value);

			value -= sign * Player.AIR_RESISTANCE * Time.deltaTime;
			if (Mathf.Sign(value) != sign)
				value = 0;
		}
	}
}
