using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Conversation : ScriptableObject
{
    [System.Serializable]
    public class Talk
    {
        public string name;
        public string[] talks;
    }
    public Talk[] talks;

 


}
