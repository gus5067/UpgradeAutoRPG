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
    public MonsterData monsterData;

    public event UnityAction<bool> onStageEnd;

    public static int characterCount;

    public static int monsterCount;

    private bool isEnd;

    private void OnEnable()
    {
        if (GameManager.Instance.curStage != null)
        {
            monsterData = GameManager.Instance.curStage;
        }
        monsterCount = monsterData.monstersPrefab.Length;
        characterCount = 1; //���� ����� �̱����̴ϱ� �ϴ��� 1�� ����

        isEnd = false;
    }
    private void Start()
    {
        curMap = GetComponent<MapGenerator>();
        StartCoroutine(SummonMonsterRoutine());
    }

    private void Update()
    {
        StageCheck();
    }
    private void Summon(int num)
    {
        switch (num%3)
        {
            case 0:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint + Vector3.up * 1.3f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint2 + Vector3.up * 1.3f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint3 + Vector3.up * 1.3f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
        }
    }

    IEnumerator SummonMonsterRoutine()
    {
        for(int i = 0; i < monsterData.monstersPrefab.Length; i++)
        {
            Summon(i);
            yield return new WaitForSeconds(6f);
        }
    }


    private void StageCheck()
    {
        if(monsterCount == 0)
        {
            onStageEnd?.Invoke(true); //���Ͱ� ������ Ʈ��� ����
            if(!isEnd)
            {
                StartCoroutine(StageRoutine());
            }
        }
        else if(characterCount ==0)
        {
            onStageEnd?.Invoke(false);//�÷��̾ ������ false ����
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
