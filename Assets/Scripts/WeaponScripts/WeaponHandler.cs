using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    Animator _anim;
    public  WeaponAim _weaponAim;
    public WeaponFireType _weaponFireType;
    public WeaponBulletType _weaponBulletType;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] AudioSource _shootSound, _reloadSound;
    public GameObject _attackPoint;
    private void Awake()
    {
        AwakeRef();
    }
    private void AwakeRef()
    {
        _anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        _anim.SetTrigger(AnimationsTag.SHOOT_TRIGGER);
    }
    public void Aim(bool canAim)
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
public enum WeaponAim
{
    NONE, SELF_AIM, AIM
}
public enum WeaponFireType
{
    SINGLE, MULTIPLE
}
public enum WeaponBulletType
{
    NONE, BULLET, ARROW, SPEAR
}
