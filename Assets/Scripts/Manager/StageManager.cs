using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private  MapGenerator curMap;
    private AudioSource curAudio;
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private SkyBoxData skyBox;

    [SerializeField]
    private GameObject[] companions;

    [HideInInspector]
    public MonsterData monsterData;

    public event UnityAction<bool> onStageEnd;

    public static int characterCount = 1;

    public static int monsterCount;

    private bool isEnd;

    private void Awake()
    {
        characterCount = 1;
        curAudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (GameManager.Instance.curStage != null)
        {
            monsterData = GameManager.Instance.curStage;
            RenderSettings.skybox = skyBox.skyBoxMaterial[GameManager.Instance.curSkyBoxNum];
            curAudio.clip = clips[GameManager.Instance.curSkyBoxNum];
            curAudio.Play();
        }
        monsterCount = monsterData.monstersPrefab.Length;

        isEnd = false;
    }
    private void Start()
    {
        curMap = GetComponent<MapGenerator>();
        StartCoroutine(SummonMonsterRoutine());
        for(int i=0; i< companions.Length; i++)
        {
            if(StageMapController.Instance.isPlayerTrigger[i])
            {
                companions[i].SetActive(true);
            }
        }
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
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint + Vector3.up * 1.2f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint2 + Vector3.up * 1.2f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(monsterData.monstersPrefab[num], curMap.monsterPoint3 + Vector3.up * 1.2f, monsterData.monstersPrefab[num].transform.rotation);
                    break;
                }
        }
    }
    private void Summon(int monNum, int num)
    {
        switch (num % 3)
        {
            case 0:
                {
                    Instantiate(monsterData.monstersPrefab[monNum], curMap.monsterPoint + Vector3.up * 1.2f, monsterData.monstersPrefab[monNum].transform.rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(monsterData.monstersPrefab[monNum], curMap.monsterPoint2 + Vector3.up * 1.2f, monsterData.monstersPrefab[monNum].transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(monsterData.monstersPrefab[monNum], curMap.monsterPoint3 + Vector3.up * 1.2f, monsterData.monstersPrefab[monNum].transform.rotation);
                    break;
                }
        }
    }

    IEnumerator SummonMonsterRoutine()
    {
        if (GameManager.Instance.curState == GameManager.State.Normal)
        {
            for (int i = 0; i < monsterData.monstersPrefab.Length; i++)
            {
                Summon(i);
                yield return new WaitForSeconds(6f);
            }
        }
        else
        {
            int i = 0;
            while(characterCount != 0)
            {
                Summon(Random.Range(0,monsterData.monstersPrefab.Length), i);
                i++;
                yield return new WaitForSeconds(8f);
            }
        }
       
    }


    private void StageCheck()
    {
        if(monsterCount == 0 && GameManager.Instance.curState == GameManager.State.Normal)
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
