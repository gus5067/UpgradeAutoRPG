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
    public void Shoot(Vector3 dir)
    {
        
        Quaternion rot = Quaternion.LookRotation(dir);

        transform.rotation = rot;

        body.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }

    private void Start()
    {
        StartCoroutine(ArrowRoutine());
        
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            target?.HitDamage(WeaponManager.Instance.minDamage);

            ObjectPooling.ReturnObject(this);
        }


    }

    IEnumerator ArrowRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        if (this.gameObject.activeSelf)
        {
            ObjectPooling.ReturnObject(this);
        }
        else
        {
            yield return null;
        }
    }
}
