using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{   
    [SerializeField] private PlayerInputSO _playerInputSO;
    [SerializeField] private PlayerController _playerController;
    
    private Vector2 _inputVector;
	private float _previousSpeed;
	
    //These fields are read and manipulated by the StateMachine actions
	[NonSerialized] public bool jumpInput;
    [NonSerialized] public bool attackInput;

	[NonSerialized] public Vector2 movementInput; //Initial input coming from the Player script
	[NonSerialized] public Vector3 movementVector; //Final movement vector, manipulated by the StateMachine actions

    public const float GRAVITY_MULTIPLIER = 5f;
    
   
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
