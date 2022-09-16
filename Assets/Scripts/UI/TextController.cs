using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private TextMeshProUGUI gemText;

    private TextMeshProUGUI stageEndText;

    private MonsterData curMapMonster;
    private void Start()
    {

        GameManager.instance.onChangeMoney += OnChangeMoney;
        moneyText.text = GameManager.instance.gameMoney.ToString();
        gemText.text = GameManager.instance.gameGem.ToString();


        if (StageManager.instance != null)
        {
            StageManager.instance.onStageEnd += OnStageEnd;
            curMapMonster = StageManager.instance.monsterData;
        }

        if (UIManager.instance != null)
        {
            UIManager.instance.stageRoundText.text = "스테이지 " + curMapMonster.stageNum.ToString() + "-" + curMapMonster.roundNum.ToString();
        }

    }

    private void OnChangeMoney()
    {
        moneyText.text = GameManager.instance.gameMoney.ToString();
        gemText.text = GameManager.instance.gameGem.ToString(); 
    }

    private void OnStageEnd(bool result)
    {
        if (result)
        {
            UIManager.instance.stageResultText.color = Color.green;
            UIManager.instance.stageResultText.text = "스테이지 클리어!!";
        }
        else if(!result)
        {
            UIManager.instance.stageResultText.color = Color.red;
            UIManager.instance.stageResultText.text = "스테이지 실패!";
        }
         
    }
}
