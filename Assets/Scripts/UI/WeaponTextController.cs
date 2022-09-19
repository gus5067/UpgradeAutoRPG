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

    private void Awake()
    {
        WeaponManager.Instance.onChangeWeaponValue += OnChangeValue;
    }
    private void Start()
    {
      
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "Damage : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;
    }

    private void OnChangeValue()
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
                weaponSpecialText.text = "흡혈 데미지";
                break;
            case 2:
                weaponNameText.color = Color.gray;
                weaponSpecialText.color = Color.gray;
                weaponSpecialText.text = "두 배 데미지";
                break;

        }
        weaponNameText.text = WeaponManager.Instance.swordName;
        weaponValueText.text = "+" + WeaponManager.Instance.weaponValue.ToString();
        weaponDamageText.text = "공격력 : " + WeaponManager.Instance.minDamage + " ~ " + WeaponManager.Instance.maxDamage;
    }
}
