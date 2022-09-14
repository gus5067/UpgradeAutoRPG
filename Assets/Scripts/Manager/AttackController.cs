using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    

    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public int minDamage;
    [HideInInspector]
    public int maxDamage;
    private void Awake()
    {
  
        minDamage = WeaponManager.instance.minDamage;
        maxDamage = WeaponManager.instance.maxDamage;
        attackSpeed = WeaponManager.instance.attackSpeed;
    }
}
