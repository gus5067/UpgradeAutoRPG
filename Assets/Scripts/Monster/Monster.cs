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
    private monsterHpController hpController;
    public event UnityAction<float> onChangeHp;

    private void Awake()
    {
        hpController = GetComponent<monsterHpController>();
    }

    public void HitDamage(float damage)
    {
        hp -= damage;
        onChangeHp?.Invoke(hp);

    }
}
