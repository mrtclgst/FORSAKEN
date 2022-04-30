using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponHandler[] weapons;
    int _currentWeaponIndex;
    private void Start()
    {
        _currentWeaponIndex = 0;
        weapons[_currentWeaponIndex].gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GetSelectedWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            GetSelectedWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            GetSelectedWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            GetSelectedWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            GetSelectedWeapon(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            GetSelectedWeapon(5);
    }
    public void GetSelectedWeapon(int _selectedWeaponIndex)
    {
        if (_currentWeaponIndex == _selectedWeaponIndex)
            return;

        weapons[_currentWeaponIndex].gameObject.SetActive(false);
        weapons[_selectedWeaponIndex].gameObject.SetActive(true);
        _currentWeaponIndex = _selectedWeaponIndex;
    }
    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[_currentWeaponIndex];
    }
}
