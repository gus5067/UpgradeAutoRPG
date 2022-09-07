using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Map"))]
public class MapData : ScriptableObject
{
    [Header("Map")]

    [SerializeField]
    private GameObject _tilePrefab;
    public GameObject tilePrefab { get { return _tilePrefab; } }

    [SerializeField]
    private Vector2 _tileSize;
    public Vector2 tileSize { get { return _tileSize; } }


}
