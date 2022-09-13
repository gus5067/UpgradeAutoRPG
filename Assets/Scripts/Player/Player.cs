using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour,IDamageable
{
    protected float hp;
    protected float initHp;
    public event UnityAction<float> onChangeHp;
    public void HitDamage(float damage)
    {
        hp -= damage;
        onChangeHp?.Invoke(hp);

    }
}
