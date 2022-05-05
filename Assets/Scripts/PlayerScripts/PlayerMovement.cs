using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    [SerializeField] Transform look_root;
    Vector3 _moveDirection; //hangi eksende gittigimizi bilmek icin
    [SerializeField] float _moveSpeed = 5f, _jumpForce = 10f, _crouchSpeed = 2.5f, _sprintSpeed = 20f;  //karakter hareket hizi
    float _gravity = 20f, _verticalVelocity, _normalSpeed = 5f;
    float _standHeight = 1.6f, _crouchHeight = 1f;
    bool _isCrouching;
    float _sprintVolume = 1f, _crouchVolume = 0.2f;
    float _walkVolumeMin = 0.3f, _walkVolumeMax = 0.6f, _walkStepDistance = 0.4f;
    float _sprintStepDistance = 0.25f, _crouchStepDistance = 0.5f;
    //stepDistanceler seslerin calma araligini temsil ediyor.
    PlayerFootSteps _playerFootSteps;
    PlayerStats _playerStats;
    float _staminaValue = 100f;
    public float _staminaTreshold = 10f;
    private void Awake()
    {
        AwakeRef();
    }
    private void Start()
    {
        NormalStepSoundSettings();
    }
    private void Update()
    {
        Sprint();
        Crouch();
        MovePlayer();
    }
    void MovePlayer()
    {
        _moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        //float x = Input.GetAxis("Horizontal");
        //yukaridaki gibi yazmamizin sebebi yazim hatalarindan kacinmak.
        _moveDirection = transform.TransformDirection(_moveDirection);
        //karakterimizin positionunu unitynin space'ine gore ayarliyoruz.
        _moveDirection = _moveSpeed * Time.deltaTime * _moveDirection;
        //time.deltatime iki frame arasini veriyor bu bize yumusak bir hareket imkani sagliyor.
        ApplyGravity();
        _characterController.Move(_moveDirection);
    }
    void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        PlayerJump();
        _moveDirection.y = _verticalVelocity * Time.deltaTime;//jumpu smootlastirdigimiz yer
    }
    private void PlayerJump()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            _verticalVelocity = _jumpForce;
    }
    void Sprint()
    {
        //stamina value
        if (_staminaValue > 0f)
        {
            if (!_isCrouching && Input.GetKeyDown(KeyCode.LeftShift) /*&& _characterController.velocity.sqrMagnitude > 1f*/)
            {
                _moveSpeed = _sprintSpeed;
                SprintStepSoundSettings();
                _staminaValue -= Time.deltaTime * _staminaTreshold;
                _playerStats.DisplayStaminaStat(_staminaValue);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = _normalSpeed;
            NormalStepSoundSettings();
        }
        if (!_isCrouching && Input.GetKey(KeyCode.LeftShift) /*&& _characterController.velocity.sqrMagnitude > 1f*/)
        {
            _staminaValue -= _staminaTreshold * Time.deltaTime;
            if (_staminaValue <= 0f)
            {
                _moveSpeed = _normalSpeed;
                NormalStepSoundSettings();
            }
            _playerStats.DisplayStaminaStat(_staminaValue);
        }
        else
        {
            _moveSpeed = _normalSpeed;
            NormalStepSoundSettings();
            if (_staminaValue != 100f)
            {
                _staminaValue += _staminaTreshold * Time.deltaTime;
                _playerStats.DisplayStaminaStat(_staminaValue);
                if (_staminaValue > 100f)
                    _staminaValue = 100f;
            }
        }
    }
    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            look_root.localPosition = new Vector3(0, _crouchHeight);
            _moveSpeed = _crouchSpeed;
            _isCrouching = true;
            CrouchStepSoundSettings();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            look_root.localPosition = new Vector3(0, _standHeight);
            _moveSpeed = _normalSpeed;
            _isCrouching = false;
            NormalStepSoundSettings();
        }
    }
    void AwakeRef()
    {
        _characterController = GetComponent<CharacterController>();
        _playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
        _playerStats = GetComponent<PlayerStats>();
    }
    void NormalStepSoundSettings()
    {
        _playerFootSteps._volumeMin = _walkVolumeMin;
        _playerFootSteps._volumeMax = _walkVolumeMax;
        _playerFootSteps._stepDistance = _walkStepDistance;
    }
    void CrouchStepSoundSettings()
    {
        _playerFootSteps._volumeMin = _crouchVolume;
        _playerFootSteps._volumeMax = _crouchVolume;
        _playerFootSteps._stepDistance = _crouchStepDistance;
    }
    void SprintStepSoundSettings()
    {
        _playerFootSteps._stepDistance = _sprintStepDistance;
        _playerFootSteps._volumeMin = _sprintVolume;
        _playerFootSteps._volumeMax = _sprintVolume;
    }
}