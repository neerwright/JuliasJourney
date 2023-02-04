using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{

    [CreateAssetMenu(fileName = "PlayerInput", menuName = "Game/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject , PlayerInputActions.IGameplayActions
    {
        
        //Gameplay
        public event UnityAction JumpEvent = delegate{};
        public event UnityAction JumpCanceledEvent = delegate{};

        public event UnityAction<Vector2> MoveEvent = delegate{};
        public event UnityAction AttackEvent = delegate{};

        public event UnityAction StartRunning = delegate{};
        public event UnityAction StoppedRunning = delegate{};
        public event UnityAction InteractEvent = delegate{};


        private PlayerInputActions _playerInputActions;

        private void OnEnable() {
            if (_playerInputActions == null)
            {
                _playerInputActions = new PlayerInputActions();
                // Tell the "gameplay" action map that we want to get told about
                // when actions get triggered.
                _playerInputActions.Gameplay.SetCallbacks(this);
            }
            _playerInputActions.Gameplay.Enable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                JumpEvent.Invoke();
            }
            if(context.phase == InputActionPhase.Canceled)
            {
                JumpCanceledEvent.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            AttackEvent.Invoke();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Performed:
                    StartRunning.Invoke();
                    break;

                case InputActionPhase.Canceled:
                    StoppedRunning.Invoke();
                    break;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            InteractEvent.Invoke();
        }

        
    }
}
