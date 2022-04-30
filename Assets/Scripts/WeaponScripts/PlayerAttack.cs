using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponManager _weaponManager;
    [SerializeField] float _fireRate = 15f, _damage = 20f;
    float _timeToNextFire;
    private void Start()
    {

    }
    private void Update()
    {
        WeaponShoot();
    }

    private void WeaponShoot()
    {
        //weapon is assault rifle
        if (_weaponManager.GetCurrentSelectedWeapon()._weaponFireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > _timeToNextFire)
            {
                _timeToNextFire = Time.time + 1f / _fireRate;//siradaki atis suresini ayarliyoruz
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
        }
        if (Input.GetMouseButton(0))
            {
                _timeToNextFire = Time.time + 1f / _fireRate;//siradaki atis suresini ayarliyoruz
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
        else
        {

        }
    }
}