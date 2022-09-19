using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Monster/MonsterData")]
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
