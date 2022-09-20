using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Monster target = other.GetComponent<Monster>();
        target?.HitDamage(WeaponManager.Instance.minDamage * 3);
    }
}
