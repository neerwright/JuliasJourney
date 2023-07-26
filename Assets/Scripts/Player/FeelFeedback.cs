using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Player;
using Com.LuisPedroFonseca.ProCamera2D;

public class FeelFeedback : MonoBehaviour
{
    [SerializeField] private PlayerInputSO _playerInputSO;
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private PlayerController _playerController;
    [Range(1f, 6f)]
    [SerializeField] private float _landingSquashPower = 2f;
    [Range(1f, 20f)]
    [SerializeField] private float _shakePower = 7f;
    [Range(1f, 20f)]
    [SerializeField] private float _shakePowerBonk = 9f;
    
    [Range(0.1f, 1f)]
    [SerializeField] private float _minimumLandingSquash = 0.4f;
    [Range(0f, 1f)]
    [SerializeField] private float _landingScreenShakeThreshold = 0.5f;
    [SerializeField] private float _bigShakeThreshold = 0.8f;
    
    public MMFeedbacks JumpFeedback;
    public MMFeedbacks LandingFeedback;
    public MMFeedbacks BonkFeedback;

    public MMFeedbacks FlipFeedback;

    //private Rigidbody _rigidbody;
    private PlayerScript _playerScript;
    private MMFeedbackSquashAndStretch SquashAndStretchLanding;
    private MMFeedbackSquashAndStretch SquashAndStretchBonk;
    private float BIG_SHAKE = 3f;
    private float _velocityLastFrame; 

    private bool _flipping = false;

    private void OnEnable()
	{
        _playerInputSO.JumpEvent += PlayJumpFeedback;
        _playerController.LandedOnGround += PlayLandingFeedback;
        _playerController.Bonked += PlayBonkFeedback;
    }

    private void OnDisable()
    {
        _playerInputSO.JumpEvent -= PlayJumpFeedback;
        _playerController.LandedOnGround -= PlayLandingFeedback;
        _playerController.Bonked -= PlayBonkFeedback;

    }

    private void Awake()
    {
        //_rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _playerScript = this.gameObject.GetComponent<PlayerScript>();
        SquashAndStretchLanding = (MMFeedbackSquashAndStretch) LandingFeedback.Feedbacks[0];
        SquashAndStretchBonk = (MMFeedbackSquashAndStretch) BonkFeedback.Feedbacks[0];
            
    }

    void Update()
    {
        if(!_flipping)
            return;

        if(!FlipFeedback.Feedbacks[0].IsPlaying)
        {
            _playerModel.transform.rotation = Quaternion.Euler(0f,90f,0f);
            _flipping = false;
        }
        
    }

    private void PlayJumpFeedback()
    {
        //gameObject.transform.localScale = Vector3.one;
        _playerModel.transform.localScale = Vector3.one;
        JumpFeedback?.PlayFeedbacks();
                

    }

    public void DoAFlip()
    {
        Debug.Log("Do Flip");
        if(!FlipFeedback.Feedbacks[0].IsPlaying)
            FlipFeedback?.PlayFeedbacks();
            _flipping = true;
    }
    private void PlayLandingFeedback()
    {
        float _velocity = _playerScript.movementVector.y;
        float fallingRatio = Mathf.InverseLerp(0, PlayerScript.MAX_FALL_SPEED, _velocity);
        float squashStrength = Mathf.Max(fallingRatio, _minimumLandingSquash);
        if (SquashAndStretchLanding)
        {
            SquashAndStretchLanding.RemapCurveOne = squashStrength * _landingSquashPower;

        }
        gameObject.transform.localScale = Vector3.one;
        _playerModel.transform.localScale = Vector3.one;
        LandingFeedback?.PlayFeedbacks();
        
        float bigShake = fallingRatio > _bigShakeThreshold ? BIG_SHAKE : 1f;
        if(fallingRatio > _landingScreenShakeThreshold)
            ProCamera2DShake.Instance.Shake(0.5f * (1/ BIG_SHAKE), new Vector2 (0.1f * _shakePower , _shakePower ) * (fallingRatio * bigShake)  , 1, 1, 10, new Vector3(0,0,0), 0.1f);
    }

    private void PlayBonkFeedback(float velocity)
    {
        if (SquashAndStretchBonk)
        {
            SquashAndStretchBonk.RemapCurveOne = (velocity / 2) + 1f;

        }
        //gameObject.transform.localScale = Vector3.one;
        BonkFeedback?.PlayFeedbacks();
        ProCamera2DShake.Instance.Shake(0.2f , new Vector2 ( _shakePowerBonk , _shakePowerBonk ) * velocity  , 1, 1, 10, new Vector3(0,0,0), 0.1f);
    }
}
