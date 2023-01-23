using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/Actions/Descend")]
public class DescendActionSO : StateActionSO<DescendAction> 
{ 
	[Tooltip("Extra Gravity when falling")]
	public float gravityMultiplier = 1f;
}

public class DescendAction : StateAction
{
	//Component references
	private Player _player;
	private PlayerController _playerController;

	private float _verticalMovement;
	private float _gravityMultiplier;
	private new DescendActionSO _originSO => (DescendActionSO)base.OriginSO; // The SO this StateAction spawned from


	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	public override void OnStateEnter()
	{
		_verticalMovement = _player.movementVector.y;

		//Prevents a double jump if the player keeps holding the jump button
		//Basically it "consumes" the input
		_player.jumpInput = false;
		_gravityMultiplier = _originSO.gravityMultiplier;
	}

	public override void OnUpdate()
	{
		//Note that deltaTime is used even though it's going to be used in ApplyMovementVectorAction, this is because it represents an acceleration, not a speed
		_verticalMovement += Physics.gravity.y *Player.GRAVITY_MULTIPLIER * _gravityMultiplier * Time.deltaTime;
		//Note that even if it's added, the above value is negative due to Physics.gravity.y

		//Cap the maximum so the player doesn't reach incredible speeds when freefalling from high positions
		_verticalMovement = Mathf.Clamp(_verticalMovement, Player.MAX_FALL_SPEED, Player.MAX_RISE_SPEED);

		_player.movementVector.y = _verticalMovement;

		if(_playerController.CollisionAbove && _verticalMovement > 0)
		{
			_verticalMovement = 0;
		}
	}
}