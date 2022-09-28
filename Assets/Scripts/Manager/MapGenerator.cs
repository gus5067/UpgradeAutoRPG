using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private MapData curMap;

    private GameObject mapCube;

    [SerializeField]
    private GameObject unvisibleWall;

    private GameObject parentGrid;
    private Vector2 mapSize;

    public Vector3 monsterPoint;
    public Vector3 monsterPoint2;
    public Vector3 monsterPoint3;
    //public Node[,] grid;

    private void Awake()
    {
        if (GameManager.Instance.curMap != null)
        {
            this.curMap = GameManager.Instance.curMap;
        }
    }
    private void OnEnable()
    {
        mapCube = curMap.tilePrefab.gameObject;
        mapSize = curMap.tileSize;
        CreateGrid();
        monsterPoint = Vector3.zero + Vector3.forward * (mapSize.y * 0.25f);
        monsterPoint2 = Vector3.zero + Vector3.forward * (mapSize.y * 0.25f) + Vector3.right * (mapSize.x * 0.25f);
        monsterPoint3 = Vector3.zero + Vector3.forward * (mapSize.y * 0.25f) - Vector3.right * (mapSize.x * 0.25f);
    }
    public void CreateGrid()
    {
        if (parentGrid != null)
            Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");

        //grid = new Node[(int)mapSize.x, (int)mapSize.y];
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * mapSize.x *0.5f - Vector3.forward * mapSize.y *0.5f;

        for (int x = 0; x < (int)mapSize.x; x++)
        {
            for (int y = 0; y < (int)mapSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (y + 0.5f);
                GameObject obj = Instantiate(mapCube, worldPoint, Quaternion.identity);
                obj.transform.parent = parentGrid.transform;
                //grid[x, y] = new Node(obj, true, x, y);
            }
        }

        for (int y = 0; y < (int)mapSize.y; y++)
        {
            Vector3 wallPoint = worldBottomLeft + Vector3.right * (0.5f) + Vector3.forward * (y + 0.5f);
            GameObject leftWall = Instantiate(unvisibleWall, wallPoint - Vector3.right, Quaternion.identity);
            GameObject  rightWall= Instantiate(unvisibleWall, wallPoint + Vector3.right * (mapSize.x), Quaternion.identity);
            leftWall.transform.parent = parentGrid.transform;
            rightWall.transform.parent = parentGrid.transform;
        }

        for (int x = 0; x < (int)mapSize.x; x++)
        {
            Vector3 wallPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (0.5f);
            GameObject leftWall = Instantiate(unvisibleWall, wallPoint - Vector3.forward, Quaternion.identity);
            GameObject rightWall = Instantiate(unvisibleWall, wallPoint + Vector3.forward * (mapSize.y), Quaternion.identity);
            leftWall.transform.parent = parentGrid.transform;
            rightWall.transform.parent = parentGrid.transform;
        }

    }

}
