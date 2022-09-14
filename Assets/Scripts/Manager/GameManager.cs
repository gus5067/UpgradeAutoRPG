using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : Singleton<GameManager>
{
    public event UnityAction onChangeMoney;
    [HideInInspector]
    public int gameMoney = 0;
    [HideInInspector]
    public int gameGem = 0;

    private int curMoney;
    private int curGem;

    private void Awake()
    {
        base.Awake();
        curMoney = gameMoney;
        curGem = gameGem;
    }

    private void Update()
    {
        if(curMoney != gameMoney | curGem != gameGem)
        {
            Debug.Log("µ·µé¾î¿Ô´Ù");
            curGem = gameGem;
            curMoney = gameMoney;
            onChangeMoney?.Invoke();
        }
    }

}
