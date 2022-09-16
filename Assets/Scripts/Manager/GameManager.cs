using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    Texture2D cursorImg;

    public MonsterData curStage = null;

    public event UnityAction onChangeMoney;
    //[HideInInspector]
    public int gameMoney = 0;
    //[HideInInspector]
    public int gameGem = 0;

    private int curMoney;
    private int curGem;

    private void Awake()
    {
        base.Awake();
       
    }

    private void Start()
    {
        curMoney = gameMoney;
        curGem = gameGem;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
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
