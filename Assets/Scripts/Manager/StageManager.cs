using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    private MapGenerator curMap;

    [SerializeField]
    public MonsterData monsterData;

    public event UnityAction<bool> onStageEnd;

    public int characterCount;

    public int monsterCount;
    private void Start()
    {
        curMap = GetComponent<MapGenerator>();
        StartCoroutine(SummonMonsterRoutine());
        monsterCount = monsterData.monstersPrefab.Length;
        characterCount = 1; //���� ����� �̱����̴ϱ� �ϴ��� 1�� ����
    }

    private void Update()
    {
        StageCheck();
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


    private void StageCheck()
    {
        if(monsterCount == 0)
        {
            onStageEnd?.Invoke(true); //���Ͱ� ������ Ʈ��� ����
        }
        else if(characterCount ==0)
        {
            onStageEnd?.Invoke(false);//�÷��̾ ������ false ����
        }
    }
}
