using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    [SerializeField]
    private GameObject arrowPrefab;

    Queue<Arrow> poolingObjectQueue = new Queue<Arrow>();

    private void Awake()
    {
        instance = this;
    }

    private void StartPooling(int startCount)
    {
        for(int i = 0; i < startCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewArrow());
        }
    }

    private Arrow CreateNewArrow()
    {
        var newObj = Instantiate(arrowPrefab).GetComponent<Arrow>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Arrow GetObject()
    {
        if (instance.poolingObjectQueue.Count > 0)
        {
            var obj = instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewArrow();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }


    public static void ReturnObject(Arrow obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue.Enqueue(obj);
    }


}
