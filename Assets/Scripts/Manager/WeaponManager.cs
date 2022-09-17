using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponManager : Singleton<WeaponManager>
{
    [HideInInspector]
    public int weaponValue = 1;
    [HideInInspector]
    public int minDamage;
    [HideInInspector]
    public int maxDamage;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public string swordName;

    private string curName;
    private int curValue;
    public event UnityAction onChangeWeaponValue;

    private void Awake()
    {
        base.Awake();
        swordName = GameManager.Instance.playerWeapon.swordName;
        minDamage = GameManager.Instance.playerWeapon.minDamage;
        maxDamage = GameManager.Instance.playerWeapon.maxDamage;
        attackSpeed = GameManager.Instance.playerWeapon.attackSpeed;
        curName = swordName;
        curValue = weaponValue;
    }

    private void Update()
    {
        if (curValue != weaponValue)
        {
            onChangeWeaponValue?.Invoke();
        }
        else
        {
            return;
        }
    }
}
