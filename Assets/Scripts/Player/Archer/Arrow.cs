using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 destination;
    private Vector3 dir;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        body.AddForce(transform.forward * 25f, ForceMode.Impulse);
        StartCoroutine(ArrowRoutine());
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            target?.HitDamage(WeaponManager.Instance.minDamage);

            ObjectPooling.poolDic["Arrow"].ReturnPool(this.gameObject);
        }


    }

    IEnumerator ArrowRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        if (this.gameObject.activeSelf)
        {
            ObjectPooling.poolDic["Arrow"].ReturnPool(this.gameObject);
        }
        else
        {
            yield return null;
        }
    }
}
