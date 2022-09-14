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
    private void Awake()
    {
        base.Awake();
        minDamage = playerWeapon.minDamage;
        maxDamage = playerWeapon.maxDamage;
        attackSpeed = playerWeapon.attackSpeed;
    }
}
