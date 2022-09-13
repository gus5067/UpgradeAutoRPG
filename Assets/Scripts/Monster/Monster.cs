using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Monster : MonoBehaviour,IDamageable
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private float initHp;
    public event UnityAction<float> onChangeHp;


   

    public void HitDamage(int damage)
    {
        hp -= damage;
        onChangeHp?.Invoke(hp);

    }
}
