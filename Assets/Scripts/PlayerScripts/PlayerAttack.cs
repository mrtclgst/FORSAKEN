using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponManager _weaponManager;
    [SerializeField] float _fireRate = 15f, _damage = 20f;
    float _timeToNextFire, _spearArrowTimer;
    [SerializeField] Animator _zoomCameraAnim;
    public bool _zoomed, _isAiming;
    Camera _mainCam;
    GameObject _crosshair;
    [SerializeField] GameObject _arrowPrefab, _spearPrefab;
    [SerializeField] Transform _arrowStartPos, _spearStartPos;
    private void Awake()
    {
        AwakeRef();
    }
    private void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }
    private void WeaponShoot()
    {
        //weapon is assault rifle
        if (_weaponManager.GetCurrentSelectedWeapon()._weaponFireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > _timeToNextFire)
            {
                _timeToNextFire = Time.time + 1f / _fireRate;   //siradaki atis suresini ayarliyoruz
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFire();
            }
        }
        //diger silahlar icin ates mekanigi
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Balta
                if (_weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                //Mermili Silahlar
                if (_weaponManager.GetCurrentSelectedWeapon()._weaponBulletType == WeaponBulletType.BULLET)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFire();
                }
                else
                {
                    if (_isAiming && _spearArrowTimer > 1f)
                    {
                        _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (_weaponManager.GetCurrentSelectedWeapon()._weaponBulletType == WeaponBulletType.ARROW)
                        {
                            ThrowArrowSpear(true);
                        }
                        else if (_weaponManager.GetCurrentSelectedWeapon()._weaponBulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowSpear(false);
                        }
                        _spearArrowTimer = 0f;
                    }
                }
            }
        }
    }
    void ZoomInAndOut()
    {
        //nisan alinabilir silahlar icin
        if (_weaponManager.GetCurrentSelectedWeapon()._weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _zoomCameraAnim.Play(AnimationsTag.ZOOM_IN_ANIM);
                _crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                _zoomCameraAnim.Play(AnimationsTag.ZOOM_OUT_ANIM);
                _crosshair.SetActive(true);
            }
        }
        //bow ve spear icin
        if (_weaponManager.GetCurrentSelectedWeapon()._weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _spearArrowTimer +=Time.time;
                _weaponManager.GetCurrentSelectedWeapon().Aim(true);
                _isAiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                _spearArrowTimer += Time.time;
                _weaponManager.GetCurrentSelectedWeapon().Aim(false);
                _isAiming = false;
            }
        }
    }
    private void AwakeRef()
    {
        _weaponManager = GetComponent<WeaponManager>();
        //bu sekilde de cameranin referansini alabilirdik. (transformun bulundugu gameobjeyi taratiyor.)
        //_zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        _crosshair = GameObject.FindGameObjectWithTag(Tags.CROSSHAIR);
        _mainCam = Camera.main;
    }
    void ThrowArrowSpear(bool throwArrow)//true for arrow false for spear  
    {   //!!BURAYI DUZELTMELÝSÝN
        if (throwArrow)
        {
            GameObject arrow = Instantiate(_arrowPrefab);
            arrow.transform.position = _arrowStartPos.position;

            arrow.GetComponent<ArrowSpearSc>().Launch(_mainCam);
        }
        else
        {
            GameObject spear = Instantiate(_spearPrefab);
            spear.transform.position = _spearStartPos.position;

            spear.GetComponent<ArrowSpearSc>().Launch(_mainCam);
            //instantiate diger overloadlarla yap!!!
        }
    }
    void BulletFire()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out raycastHit))
        {
            if (raycastHit.transform.tag == Tags.ENEMY_TAG)
            {
                //hasar vuruyoruz!
                raycastHit.transform.GetComponent<HealthScript>().ApplyDamage(_damage);
            }
        }
    }
}