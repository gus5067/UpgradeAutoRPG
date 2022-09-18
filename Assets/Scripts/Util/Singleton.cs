using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //public static T instance = null;

    //public void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = (T)this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj;
                obj = GameObject.Find(typeof(T).Name);
                if(obj == null)
                {
                    obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                else
                {
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}