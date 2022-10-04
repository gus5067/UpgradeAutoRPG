using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public enum State { Normal, Infinite}
    public State curState;
    [SerializeField]
    public Vector3 characterPos;

    [SerializeField]
    public Vector3 characterRotation;
    public int curSkyBoxNum;
    //[SerializeField]
    //public Sword playerWeapon;

    public MonsterData curStage = null;

    public MapData curMap = null;

    public event UnityAction onChangeMoney;
    //[HideInInspector]
    public int gameMoney = 200;
    //[HideInInspector]
    public int gameGem;

    private int curMoney;
    private int curGem;

    public float curBGM = 1;
    public float curSFX = 1;
    public float curVoice = 1;

    public Dictionary<string, int> gameCount = new Dictionary<string, int>();

    public void AddDictionary(string name)
    {
        if(!gameCount.ContainsKey(name))
        {
            gameCount.Add(name, 1);
        }
        else
        {
            gameCount[name]++;
        }
        
    }
    private void Start()
    {
        curMoney = gameMoney;
        curGem = gameGem;

        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "StartScene")
        //{
        //    AddDictionary("게임 접속 횟수");
        //}
    }

    private void Update()
    {
        if(curMoney != gameMoney | curGem != gameGem)
        {
            curGem = gameGem;
            curMoney = gameMoney;
            onChangeMoney?.Invoke();
        }
    }



}
