using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapTrigger : MonoBehaviour
{
    [SerializeField]
    private int num;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Ãæµ¹ : " + num + "¹ø");
            MapTriggerEnter(num);
        }
    }

    private void MapTriggerEnter(int i)
    {
        StageMapController.Instance.isTrigger[i] = true;
    }
}
