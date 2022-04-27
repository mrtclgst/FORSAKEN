using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootSteps : MonoBehaviour
{
    AudioSource _footStepSound;
    [SerializeField] AudioClip[] _footStepClip;
    CharacterController _characterController;
    [HideInInspector] public float _volumeMin, _volumeMax, _stepDistance;
    float _accumulatedDistance;
    private void Awake()
    {
        AwakeRef();
        
    }
    private void Update()
    {
        CheckToPlayStepSound();
    }

    private void CheckToPlayStepSound()
    {
        if (!_characterController.isGrounded)
            return;
        if (_characterController.velocity.sqrMagnitude>0)//sqrmagnitude vectorun uzunlugunun getiriyor.                             
        //If you're doing distance checks, use sqrMagnitude and just square the
        //distance you're checking against. If you need to know the actual distance,
        //then magnitude will work.
        //The reason for using sqrMagnitude instead of magnitude is because magnitude
        //has to do a square root operation, which is pretty expensive, much more 
        //so than a multiplication.
        {
            //accumulateddistance is the value how far can we go
            //e.g. make step sprint or crouch
            //until we play the footstep sound
            _accumulatedDistance += Time.deltaTime;
            if (_accumulatedDistance>_stepDistance)
            {
                _footStepSound.volume = Random.Range(_volumeMin, _volumeMax);
                _footStepSound.clip = _footStepClip[Random.Range(0, _footStepClip.Length)];
                _footStepSound.Play();
                _accumulatedDistance = 0f;
            }
        }
        else
            _accumulatedDistance = 0f;
    }
    private void AwakeRef()
    {
        _footStepSound = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
    }
}
