using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField]
    public Sword playerWeapon;

    public int minDamage;

    public int maxDamage;

    public float attackSpeed;

    public string swordName;
    private void Awake()
    {
        base.Awake();
        swordName = playerWeapon.swordName;
        minDamage = playerWeapon.minDamage;
        maxDamage = playerWeapon.maxDamage;
        attackSpeed = playerWeapon.attackSpeed;
    }
}
