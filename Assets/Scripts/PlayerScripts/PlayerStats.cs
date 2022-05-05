using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Image _healthStat, _staminaStat;
    public void DisplayHealthStat(float _healthValue)
    {
        _healthValue /= 100f;//100e bolunmus hali lazim bizim fillimiz 0 1 arasi calisiyor.
        _healthStat.fillAmount = _healthValue;
    }
    public void DisplayStaminaStat(float _staminaValue)
    {
        _staminaValue /= 100f;//100e bolunmus hali lazim bizim fillimiz 0 1 arasi calisiyor.
        _staminaStat.fillAmount = _staminaValue;
    }
}