using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private  MapGenerator curMap;

    [HideInInspector]
    public static MonsterData monsterData;

    public static event UnityAction<bool> onStageEnd;

    public static int characterCount;

    public static int monsterCount;

    private bool isEnd;
    private void Start()
    {
        if (GameManager.Instance.curStage != null)
        {
            monsterData = GameManager.Instance.curStage;
        }
        curMap = GetComponent<MapGenerator>();
        StartCoroutine(SummonMonsterRoutine());
        monsterCount = monsterData.monstersPrefab.Length;
        characterCount = 1; //아직 동료는 미구현이니깐 일단은 1로 시작

        isEnd = false;
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
            onStageEnd?.Invoke(true); //몬스터가 없으면 트루로 보냄
            if(!isEnd)
            {
                StartCoroutine(StageRoutine());
            }
        }
        else if(characterCount ==0)
        {
            onStageEnd?.Invoke(false);//플레이어가 없으면 false 보냄
            if (!isEnd)
            {
                StartCoroutine(StageRoutine());
            }
        }
    }

    IEnumerator StageRoutine()
    {
        isEnd = true;
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("UpgradeTest");
    }
}
