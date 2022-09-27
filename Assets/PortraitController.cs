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

        if(StageMapController.Instance.isPlayerTrigger[0] == false && StageMapController.Instance.isPlayerTrigger[1] == true)
        {
            portraits[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(150.4f, portraits[1].GetComponent<RectTransform>().anchoredPosition.y);
        }
        else
        {
            portraits[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(251, portraits[1].GetComponent<RectTransform>().anchoredPosition.y);
        }
    }
}
