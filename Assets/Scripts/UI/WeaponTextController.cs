using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponTextController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI weaponNameText;

    [SerializeField]
    private TextMeshProUGUI weaponValueText;

    [SerializeField]
    private TextMeshProUGUI weaponDamageText;
    private void Start()
    {
        WeaponManager.instance.onChangeWeaponValue += OnChangeValue;
        weaponNameText.text = WeaponManager.instance.swordName;
        weaponValueText.text = "+" + WeaponManager.instance.weaponValue.ToString();
        weaponDamageText.text = "Damage : " + WeaponManager.instance.minDamage + " ~ " + WeaponManager.instance.maxDamage;
    }

    private void OnChangeValue()
    {
        weaponNameText.text = WeaponManager.instance.swordName;
        weaponValueText.text = "+" + WeaponManager.instance.weaponValue.ToString();
        weaponDamageText.text = "°ø°Ý·Â : " + WeaponManager.instance.minDamage + " ~ " + WeaponManager.instance.maxDamage;
    }
}
