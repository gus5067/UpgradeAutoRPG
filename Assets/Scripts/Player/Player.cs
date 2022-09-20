using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour,IDamageable
{
    public event UnityAction<int> onChangeHp;



    public void HitDamage(int damage)
    {
        onChangeHp?.Invoke(damage);

    }

    public void DieCount()
    {
        StageManager.characterCount--;
    }

    
}
