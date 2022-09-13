using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    private MapGenerator curMap;

    [SerializeField]
    private MonsterData monsterData;

    private void Awake()
    {
        curMap = GetComponent<MapGenerator>();
    }

    private void Start()
    {
        StartCoroutine(SummonMonsterRoutine());
    }

    private void Summon(int num)
    {
        int randomNum = Random.Range(0, 3);
        switch (randomNum)
        {
            case 0:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint + Vector3.up * 1.5f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint2 + Vector3.up * 1.5f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint3 + Vector3.up * 1.5f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
        }
    }

    IEnumerator SummonMonsterRoutine()
    {
        for(int i = 0; i < monsterData.monstersPrefab.Length; i++)
        {
            Summon(i);
            yield return new WaitForSeconds(10f);
        }
    }

}
