using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPool
{
    public GameObject prafab;
    public int initSize;

    private GameObject parentsObj;

    public Queue<GameObject> pool = new Queue<GameObject>();
    public void Init()
    {
        parentsObj = new GameObject(prafab.name + "_Pool");
        AddPool(initSize);
    }
    public void AddPool(int count = 1)
    {
        for(int i=0; i<count;i++)
        {
            GameObject temp = GameObject.Instantiate(prafab, parentsObj.transform);
            temp.SetActive(false);
            pool.Enqueue(temp);

        }
    }
    public void GetPool(Vector3 pos, Quaternion rot)
    {
        GameObject temp = pool.Dequeue();

        temp.transform.parent = null;
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);

    }

    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = parentsObj.transform;
        pool.Enqueue(obj);
    }

}

[System.Serializable]
public class ObjectPooling : MonoBehaviour
{
    public ObjectPool[] pools;
    public static Dictionary<string, ObjectPool> poolDic = new Dictionary<string, ObjectPool>();
    private void Start()
    {
        foreach(var pool in pools)
        {
            if(poolDic.ContainsKey(pool.prafab.name) == false)
            {
                pool.Init();
                poolDic.Add(pool.prafab.name, pool);
            }
            else
            {
                poolDic.Clear();
                pool.Init();
                poolDic.Add(pool.prafab.name, pool);
            }
        }
    }


}
