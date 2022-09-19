using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster/DropItem")]
public class DropItemData : ScriptableObject
{
    [SerializeField]
    public int minGold;

    [SerializeField]
    public int maxGold;

    [SerializeField]
    public int gem;
}
