using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{


    public Vector3 curPos = Vector3.zero;
    public Quaternion curRotation = Quaternion.identity;

    [SerializeField]
    public Vector3 characterPos;

    [SerializeField]
    public Vector3 characterRotation;

    //[SerializeField]
    //public Sword playerWeapon;

    public MonsterData curStage = null;

    public event UnityAction onChangeMoney;
    //[HideInInspector]
    public int gameMoney = 200;
    //[HideInInspector]
    public int gameGem;

    private int curMoney;
    private int curGem;
    private void Start()
    {
        curMoney = gameMoney;
        curGem = gameGem;

 

        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "StageSelectTest")
        //{
        //    player = FindObjectOfType<PlayerNavMove>().gameObject;
        //    if(characterPos != null)
        //    {
        //        player.transform.position = characterPos;
        //        player.transform.rotation = Quaternion.Euler(characterRotation);
        //    }
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
