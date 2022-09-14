using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Sword curSword;

    public float attackSpeed;

    [HideInInspector]
    public int minDamage;
    [HideInInspector]
    public int maxDamage;
    private void Awake()
    {
        minDamage = curSword.minDamage;
        maxDamage = curSword.maxDamage;
        attackSpeed = curSword.attackSpeed;
    }
}
