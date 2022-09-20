using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/TrailData")]
public class TrailData : ScriptableObject
{

    [SerializeField]
    public Material[] trails;
}
