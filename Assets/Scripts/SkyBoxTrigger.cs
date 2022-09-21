using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxTrigger : MonoBehaviour
{
    [SerializeField]
    private int num;
    private MapManager mapManager;

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            mapManager.ChangeSkyBox(num);
        }
    }
}
