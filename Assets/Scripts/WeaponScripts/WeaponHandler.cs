using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    Animator _anim;
    [SerializeField] WeaponAim _weaponAim;
    [SerializeField] WeaponFireType _weaponFireType;
    [SerializeField] WeaponBulletType _weaponBulletType;
    [SerializeField] GameObject _muzzleFlash, _attackPoint;
    [SerializeField] AudioSource _shootSound, _reloadSound;
    private void Awake()
    {
        AwakeRef();
    }
    private void AwakeRef()
    {
        _anim = GetComponent<Animator>();
    }
    void ShootAnimation()
    {
        _anim.SetTrigger(AnimationsTag.ATTACK_TRIGGER);
    }
    void Aim(bool canAim)
    {
        _anim.SetBool(AnimationsTag.AIM_PARAMETER, canAim);
    }
    void TurnOnMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }
    //yukaridakilerin yerine bir switch case de yazilabilir.
    //ayriyeten instantiate metodu ile de muzzleflash effectini uygulayabiliriz.
    void PlayShootSound()
    {
        _shootSound.Play();
    }
    void PlayReloadSound()
    {
        _reloadSound.Play();
    }
    void TurnOnAttackPoint()
    {
        _attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (_attackPoint.activeInHierarchy)
            _attackPoint.SetActive(false);
    }
}
enum WeaponAim
{
    NONE, SELF_AIM, AIM
}
enum WeaponFireType
{
    SINGLE, MULTIPLE
}
enum WeaponBulletType
{
    NONE, BULLET, ARROW, SPEAR
}
