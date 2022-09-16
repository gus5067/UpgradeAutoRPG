using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterData : ScriptableObject
{
    [SerializeField, Header("Monsters")]
    public GameObject[] monstersPrefab;

    [Header("Stage")]
    [SerializeField]
    public int stageNum;

    [SerializeField]
    public int roundNum;

}
