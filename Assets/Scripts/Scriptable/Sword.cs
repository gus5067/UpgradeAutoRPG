using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Sword")]
public class Sword : ScriptableObject
{
    [SerializeField]
    public int minDamage;
    [SerializeField]
    public int maxDamage;
    [SerializeField]
    public float attackSpeed;
}
