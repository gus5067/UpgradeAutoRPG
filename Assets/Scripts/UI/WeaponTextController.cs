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

    private void Start()
    {
        WeaponManager.instance.onChangeWeaponValue += OnChangeValue;
        weaponNameText.text = WeaponManager.instance.swordName;
        weaponValueText.text = "+" + WeaponManager.instance.weaponValue.ToString();
    }

    private void OnChangeValue()
    {
        weaponNameText.text = WeaponManager.instance.swordName;
        weaponValueText.text = "+" + WeaponManager.instance.weaponValue.ToString();
    }
}
