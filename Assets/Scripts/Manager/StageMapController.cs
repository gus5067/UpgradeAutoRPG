using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMapController : Singleton<StageMapController>
{
    [SerializeField]
    public bool[] isTrigger = new bool[4];

    [SerializeField]
    public bool[] isPlayerTrigger = new bool[2];

    [SerializeField]
    public bool[] questTrigger = new bool[2];
}
