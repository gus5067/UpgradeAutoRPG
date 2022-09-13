using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Sword curSword;

    public float attackSpeed;

    public int minDamage;

    public int maxDamage;
    private void Awake()
    {
        minDamage = curSword.minDamage;
        maxDamage = curSword.maxDamage;
        attackSpeed = curSword.attackSpeed;
    }
}
