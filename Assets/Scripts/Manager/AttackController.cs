using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    private Sword curSword;

    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public int minDamage;
    [HideInInspector]
    public int maxDamage;
    private void Awake()
    {
        curSword = WeaponManager.instance.playerWeapon;
        minDamage = curSword.minDamage;
        maxDamage = curSword.maxDamage;
        attackSpeed = curSword.attackSpeed;
    }
}
