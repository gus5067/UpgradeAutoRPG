using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    Texture2D cursorImg;

    public Vector3 curPos = Vector3.zero;
    public Quaternion curRotation = Quaternion.identity;

    [SerializeField]
    private Transform character;


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

        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "StageSelectTest")
        {
            if (curPos != Vector3.zero)
            {
                character.position = new Vector3(curPos.x + 0.3f, curPos.y, curPos.z);
                character.rotation = curRotation;
            }
        }
       
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
