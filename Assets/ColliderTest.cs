using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private Collider[] colliders;
    private void Start()
    {
        colliders = Physics.OverlapSphere(transform.position, 5, 1<<6);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
        }
    }

}
