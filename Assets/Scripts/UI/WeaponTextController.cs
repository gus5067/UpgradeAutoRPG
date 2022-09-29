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

    [SerializeField]
    private TextMeshProUGUI weaponSpecialText;

    private void Start()
    {
        WeaponManager.Instance.onChangeWeaponValue += OnChangeValue;
        ChangeColor();
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "공격력 : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;

    }

    private void OnChangeValue()
    {

        ChangeColor();
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "공격력 : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;
    }

    private void ChangeColor()
    {
        switch (WeaponManager.Instance.WeaponStateNum)
        {
            case 0:
                weaponNameText.color = Color.white;
                weaponSpecialText.text = " ";
                break;
            case 1:
                weaponNameText.color = Color.red;
                weaponSpecialText.color = Color.red;
                weaponSpecialText.text = "흡혈 효과";
                break;
            case 2:
                weaponNameText.color = Color.gray;
                weaponSpecialText.color = Color.gray;
                weaponSpecialText.text = "최대 데미지";
                break;

        }
    }
}
