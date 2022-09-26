using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ViewDetector))]
public class WormShot : MonoBehaviour
{
    [SerializeField]
    private GameObject Vfx;
    private ViewDetector viewDetector;
    private Rigidbody body; 
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        viewDetector = GetComponent<ViewDetector>();
    }

    private void Start()
    {
        viewDetector.FindTarget();
        if(viewDetector.target != null)
        {
            Vector3 Dir = viewDetector.target.transform.position - transform.position;

            body.velocity = Dir.normalized * 10f;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vfx.SetActive(true);
        Vfx.transform.SetParent(null);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, 1 << 7);
        foreach (Collider collider in colliders)
        {
            Player target = collider.gameObject.GetComponent<Player>();
            target?.HitDamage(10);
        }
        ObjectPooling.ReturnWormObject(this);   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    //IEnumerator DestroyRoutine(float num)
    //{
    //    yield return new WaitForSeconds(num);
    //    ObjectPooling.ReturnWormObject(this);
    //}


}
