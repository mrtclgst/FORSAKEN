using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _screamSound, _dieSound;
    [SerializeField] AudioClip[] _attackSounds;
    private void Awake()
    {
        AwakeRef();
    }
    public void PlayScreamSound()
    {
        _audioSource.clip = _screamSound;
        _audioSource.Play();
    }
    public void PlayAttackSound()
    {
        _audioSource.clip = _attackSounds[UnityEngine.Random.Range(0, _attackSounds.Length)];
        _audioSource.Play();
    }
    public void PlayDeadSound()
    {
        _audioSource.clip = _dieSound;
        _audioSource.Play();
    }



    private void AwakeRef()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
