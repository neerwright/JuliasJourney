using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Player;
using Com.LuisPedroFonseca.ProCamera2D;

public class FeelFeedback : MonoBehaviour
{
    [SerializeField] private PlayerInputSO _playerInputSO;
    public MMFeedbacks JumpFeedback;
    public MMFeedbacks LandingFeedback;

    private Rigidbody _rigidbody;
    private PlayerScript _playerScript;
    private float _velocityLastFrame; 

    private void OnEnable()
	{
        _playerInputSO.JumpEvent += PlayJumpFeedback;
    }

    private void OnDisable()
    {
        _playerInputSO.JumpEvent -= PlayJumpFeedback;

    }

    private void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _playerScript = this.gameObject.GetComponent<PlayerScript>();
    }

    void Update()
    {
    }

    private void PlayJumpFeedback()
    {
        JumpFeedback?.PlayFeedbacks();
    }

    private void PlayLandingFeedback()
    {
        float _velocity = _playerScript.movementVector.y;
        LandingFeedback?.PlayFeedbacks();
        ProCamera2D.Instance.Shake("LandingShake");
    }
}
