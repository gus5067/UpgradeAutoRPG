using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private MapData curMap;

    private GameObject mapCube;

    private GameObject parentGrid;
    private Vector2 mapSize;

    Node[,] grid;

    private void Awake()
    {
        mapCube = curMap.tilePrefab.gameObject;
        mapSize = curMap.tileSize;
        CreateGrid();
    }

    public void CreateGrid()
    {
        if (parentGrid != null)
            Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");

        grid = new Node[(int)mapSize.x, (int)mapSize.y];
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * mapSize.x / 2 - Vector3.forward * mapSize.y / 2;

        for (int x = 0; x < (int)mapSize.x; x++)
        {
            for (int y = 0; y < (int)mapSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (y + 0.5f);
                GameObject obj = Instantiate(mapCube, worldPoint, Quaternion.identity);
                obj.transform.parent = parentGrid.transform;
                grid[x, y] = new Node(obj, true, x, y);
            }
        }
    }

}
