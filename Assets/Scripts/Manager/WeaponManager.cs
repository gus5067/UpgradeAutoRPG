using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField]
    private Sword playerWeapon;
    [SerializeField]
    public int WeaponStateNum = 0;
    [HideInInspector]
    public int weaponValue = 1;
    public int minDamage;
    public int maxDamage;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public string swordName;

    private string curName;
    private int curValue;
    public event UnityAction onChangeWeaponValue;

    private void OnEnable()
    {
        swordName = playerWeapon.swordName;
        minDamage = playerWeapon.minDamage;
        maxDamage = playerWeapon.maxDamage;
        attackSpeed = playerWeapon.attackSpeed;
        curName = swordName;
        curValue = weaponValue;
    }
    private void Update()
    {
        if (curValue != weaponValue)
        {
            onChangeWeaponValue?.Invoke();
            curValue = weaponValue;
        }
        else if(curName != swordName)
        {
            onChangeWeaponValue?.Invoke();
            curName = swordName;
        }
        else
        {
            return;
        }
    }
}
