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
        if(other.tag == "Player")
        {
            IDamageable target = other.GetComponent<IDamageable>();
            target?.HitDamage(num);
        }
    }
}
