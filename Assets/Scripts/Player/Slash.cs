using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField]
    private int num;
    private void OnParticleCollision(GameObject other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        target?.HitDamage(num * 2);
    }
}
