using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Player;
using Com.LuisPedroFonseca.ProCamera2D;

public class FeelFeedback : MonoBehaviour
{
    [SerializeField] private PlayerInputSO _playerInputSO;
    [SerializeField] private PlayerController _playerController;
    [Range(1f, 6f)]
    [SerializeField] private float _landingSquashPower = 2f;
    [Range(1f, 20f)]
    [SerializeField] private float _shakePower = 7f;
    [Range(0.1f, 1f)]
    [SerializeField] private float _minimumLandingSquash = 0.4f;
    [Range(0f, 1f)]
    [SerializeField] private float _landingScreenShakeThreshold = 0.5f;
    [SerializeField] private float _bigShakeThreshold = 0.8f;
    
    public MMFeedbacks JumpFeedback;
    public MMFeedbacks LandingFeedback;

    private Rigidbody _rigidbody;
    private PlayerScript _playerScript;
    private MMFeedbackSquashAndStretch SquashAndStretchFeelComponent;
    private float BIG_SHAKE = 3f;
    private float _velocityLastFrame; 

    private void OnEnable()
	{
        _playerInputSO.JumpEvent += PlayJumpFeedback;
        _playerController.LandedOnGround += PlayLandingFeedback;
    }

    private void OnDisable()
    {
        _playerInputSO.JumpEvent -= PlayJumpFeedback;
        _playerController.LandedOnGround -= PlayLandingFeedback;

    }

    private void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _playerScript = this.gameObject.GetComponent<PlayerScript>();

        
        
            
                
                SquashAndStretchFeelComponent = (MMFeedbackSquashAndStretch) LandingFeedback.Feedbacks[0];
                Debug.Log(SquashAndStretchFeelComponent);
            
        


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
        float fallingRatio = Mathf.InverseLerp(0, PlayerScript.MAX_FALL_SPEED, _velocity);
        float squashStrength = Mathf.Max(fallingRatio, _minimumLandingSquash);
        if (SquashAndStretchFeelComponent)
        {
            SquashAndStretchFeelComponent.RemapCurveOne = squashStrength * _landingSquashPower;

        }
        
        LandingFeedback?.PlayFeedbacks();
        
        float bigShake = fallingRatio > _bigShakeThreshold ? BIG_SHAKE : 1f;
        Debug.Log(_shakePower * (fallingRatio * bigShake) );
        if(fallingRatio > _landingScreenShakeThreshold)
            ProCamera2DShake.Instance.Shake(0.5f * (1/ BIG_SHAKE), new Vector2 (0.1f * _shakePower , _shakePower ) * (fallingRatio * bigShake)  , 1, 1, 10, new Vector3(0,0,0), 0.1f);
    }
}
