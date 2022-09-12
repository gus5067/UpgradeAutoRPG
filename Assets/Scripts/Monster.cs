using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Monster : MonoBehaviour,IDamageable
{
    private float hp;
    private float initHp;
    private HpController hpController;

    public event UnityAction<float> onChangeHp;

    private void Start()
    {
        hpController = GetComponent<HpController>();

        hp = hpController.hp;
        initHp = hpController.initHp; 
    }

    public void HitDamage(float damage)
    {
        hp -= damage;
        onChangeHp?.Invoke(hp);

    }
}
