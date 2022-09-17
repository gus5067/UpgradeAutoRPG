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

    private void Start()
    {
        minDamage = WeaponManager.Instance.minDamage;
        maxDamage = WeaponManager.Instance.maxDamage;
        attackSpeed = WeaponManager.Instance.attackSpeed;
    }
    private void Update()
    {
        if(WeaponManager.Instance != null)
        {
            if (minDamage != WeaponManager.Instance.minDamage | maxDamage != WeaponManager.Instance.maxDamage | attackSpeed != WeaponManager.Instance.attackSpeed)
            {
                minDamage = WeaponManager.Instance.minDamage;
                maxDamage = WeaponManager.Instance.maxDamage;
                attackSpeed = WeaponManager.Instance.attackSpeed;
            }
            else
            {
                return;
            }
        }
        
    }
}
