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


    }

    private void Start()
    {
        StartCoroutine(ArrowRoutine());
        body.AddForce(transform.forward * 25f,ForceMode.Impulse);
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
        if(!this.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(5f);
            ObjectPooling.ReturnObject(this);
        }
    }
}
