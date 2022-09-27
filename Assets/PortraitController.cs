using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] portraits;

    private void Start()
    {
        for (int i = 0; i < StageMapController.Instance.isPlayerTrigger.Length; i++)
        {
            if (StageMapController.Instance.isPlayerTrigger[i])
            {
                portraits[i].SetActive(true);
            }
        }
    }
}
