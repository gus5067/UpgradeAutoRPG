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
        WeaponManager.Instance.onChangeWeaponValue += OnChangeValue;
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "Damage : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;
    }

    private void OnChangeValue()
    {
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "°ø°Ý·Â : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;
    }
}
