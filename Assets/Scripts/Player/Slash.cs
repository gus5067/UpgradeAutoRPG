using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField]
    private int num;

    private List<GameObject> colliderList;
    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == 6)
        {
            colliderList.Add(other);
        }
    
        
        //IDamageable target = other.GetComponent<IDamageable>();
        //target?.HitDamage(num * 2);
    }

    private void DamageList()
    {

    }
}
