using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{   
    [SerializeField] private PlayerInputSO _playerInputSO;
	
    
    private Vector2 _inputVector;
	private float _previousSpeed;
	
    //These fields are read and manipulated by the StateMachine actions
	[NonSerialized] public bool jumpInput;
    [NonSerialized] public bool attackInput;
	[NonSerialized] public bool grounded;

	[NonSerialized] public Vector2 movementInput; //Initial input coming from the Player script
	[NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
	[NonSerialized]	public Rigidbody2D rb2d;

    public const float GRAVITY_MULTIPLIER = 3f;
    public const float MAX_SPEED = 3f;
	public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
	public const float GRAVITY_DIVIDER = .6f;
	public const float MAX_FALL_SPEED = -50f;
	public const float MAX_RISE_SPEED = 100f;
	public const float AIR_RESISTANCE = 5f;

   
    private void OnEnable()
	{
		_playerInputSO.JumpEvent += OnJumpInitiated;
		_playerInputSO.JumpCanceledEvent += OnJumpCanceled;
		_playerInputSO.MoveEvent += OnMove;
		_playerInputSO.AttackEvent += OnStartedAttack;
		//...
	}

	//Removes all listeners to the events coming from the InputReader script
	private void OnDisable()
	{
		_playerInputSO.JumpEvent -= OnJumpInitiated;
		_playerInputSO.JumpCanceledEvent -= OnJumpCanceled;
		_playerInputSO.MoveEvent -= OnMove;
		_playerInputSO.AttackEvent -= OnStartedAttack;
		//...
	}

    // Update is called once per frame
    void Update()
    {
        RecalculateMovement();
    }

    private void RecalculateMovement()
	{
		movementInput = _inputVector;
    }

    //---- EVENT LISTENERS ----

	private void OnMove(Vector2 movement)
	{

		_inputVector = movement;
	}

	private void OnJumpInitiated()
	{
		jumpInput = true;
	}

	private void OnJumpCanceled()
	{
		jumpInput = false;
	}

    private void OnStartedAttack() => attackInput = true;

}
