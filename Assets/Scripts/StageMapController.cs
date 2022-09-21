using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMapController : Singleton<StageMapController>
{
    [SerializeField]
    public bool[] isTrigger = new bool[4]; 
}
