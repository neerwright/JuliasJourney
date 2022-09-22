using UnityEngine;


[CreateAssetMenu(fileName = "Ascend", menuName = "State Machine/Actions/Ascend")]
public class AscendActionSO : StateActionSO<AscendAction>
{
	[Tooltip("The initial upwards push when pressing jump. This is injected into verticalMovement, and gradually cancelled by gravity")]
	public float initialJumpForce = 6f;
}

public class AscendAction : StateAction
{
	//Component references
	private Player _player;

	private float _verticalMovement;
	private float _gravityContributionMultiplier;
	private new AscendActionSO _originSO => (AscendActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	public override void OnStateEnter()
	{
		_verticalMovement = _originSO.initialJumpForce;
	}

	public override void OnUpdate()
	{
		_gravityContributionMultiplier += Player.GRAVITY_COMEBACK_MULTIPLIER;
		_gravityContributionMultiplier *= Player.GRAVITY_DIVIDER; //Reduce the gravity effect

		//Note that deltaTime is used even though it's going to be used in ApplyMovementVectorAction, this is because it represents an acceleration, not a speed
		_verticalMovement += Physics.gravity.y * Player.GRAVITY_MULTIPLIER * _gravityContributionMultiplier * Time.deltaTime;
		//Note that even if it's added, the above value is negative due to Physics.gravity.y

		_player.movementVector.y = _verticalMovement;
	}
}