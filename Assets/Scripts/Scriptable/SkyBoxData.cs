using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Map/SkyBox")]
public class SkyBoxData : ScriptableObject
{
    [SerializeField]
    private Material[] _skyBoxMaterial;

    public Material[] skyBoxMaterial { get { return _skyBoxMaterial; } }
}
