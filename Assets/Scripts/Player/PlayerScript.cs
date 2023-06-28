using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scriptables;

namespace Player
{

	public class PlayerScript : MonoBehaviour
	{   
		[SerializeField] private PlayerInputSO _playerInputSO;
		[SerializeField] private GameEvent _playerPressedInteractEvent;
		[SerializeField] private GameEvent _playerCanceledInteractEvent;
		
		private Vector2 _inputVector;
		private float _previousSpeed;
		
		//These fields are read and manipulated by the StateMachine actions
		[NonSerialized] public bool jumpInput = false;
		[NonSerialized] public bool attackInput = false;
		[NonSerialized] public bool grounded = false;
		[NonSerialized] public bool interactInput = false;

		[NonSerialized] public Vector2 movementInput; //Initial input coming from the Player script
		[NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
		[NonSerialized]	public Rigidbody2D rb2d;


		public const float GRAVITY_MULTIPLIER = 3f;
		public const float MAX_SPEED = 3f;
		public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
		public const float GRAVITY_DIVIDER = .6f;
		public const float MAX_FALL_SPEED = -50f;
		public const float MAX_RISE_SPEED = 100f;
		public const float AIR_RESISTANCE = 2f;

		public const float INPUT_BUFFER = 1.6f;

		private bool _coRoutineIsPlaying = false;
		private IEnumerator coroutine;

		private void OnEnable()
		{
			
			_playerInputSO.StoppedInteractEvent += OnInteractCanceled;
			_playerInputSO.JumpEvent += OnJumpInitiated;
			_playerInputSO.JumpCanceledEvent += OnJumpCanceled;
			_playerInputSO.MoveEvent += OnMove;
			_playerInputSO.AttackEvent += OnStartedAttack;
			_playerInputSO.InteractEvent += OnInteract;

			//...
			coroutine = WaitUntilInteractCanBePressedAgain(INPUT_BUFFER);
		}

		//Removes all listeners to the events coming from the InputReader script
		private void OnDisable()
		{
			_playerInputSO.StoppedInteractEvent -= OnInteractCanceled;
			_playerInputSO.JumpEvent -= OnJumpInitiated;
			_playerInputSO.JumpCanceledEvent -= OnJumpCanceled;
			_playerInputSO.MoveEvent -= OnMove;
			_playerInputSO.AttackEvent -= OnStartedAttack;
			_playerInputSO.InteractEvent -= OnInteract;
			//...
		}

		// Update is called once per frame
		void Update()
		{
			RecalculateMovement();
			FlipSprite();
		}

		private void FlipSprite()
		{
			
			bool playerHasHorizontalSpeed = Mathf.Abs(movementVector.x) > Mathf.Epsilon;
			if (playerHasHorizontalSpeed)
			{
				transform.localScale = new Vector2 (Mathf.Sign(movementVector.x), 1f);
			}
				
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
		
		private void OnInteract()
		{

			if(!_coRoutineIsPlaying)
			{
				interactInput = true;
				_playerPressedInteractEvent?.Raise();
				coroutine = WaitUntilInteractCanBePressedAgain(INPUT_BUFFER);
        		StartCoroutine(coroutine);
			}

		} 
		
		private void OnInteractCanceled()
		{
			_playerCanceledInteractEvent?.Raise();
		}	


		IEnumerator WaitUntilInteractCanBePressedAgain(float seconds)
		{			
			_coRoutineIsPlaying = true;
			yield return new WaitForSeconds(seconds);
			_coRoutineIsPlaying = false;
			interactInput = false;
		}

	}
}