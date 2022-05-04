using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _wooshSound;
    void PlayWooshSound()
    {
        _audioSource.clip = _wooshSound[Random.Range(0,_wooshSound.Length)];
        _audioSource.Play();
    }
}
