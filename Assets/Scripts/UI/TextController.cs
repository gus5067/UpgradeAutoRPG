using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField]
    private StageManager stageManager;

    [SerializeField]
    private UIManager uimanager;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private TextMeshProUGUI gemText;

    private TextMeshProUGUI stageEndText;

    private MonsterData curMapMonster;
    private void Start()
    {
        GameManager.Instance.onChangeMoney += OnChangeMoney;
        if(stageManager!=null)
        {
            stageManager.onStageEnd += OnStageEnd;
        }
        moneyText.text = GameManager.Instance.gameMoney.ToString();
        gemText.text = GameManager.Instance.gameGem.ToString();
        if(stageManager != null)
        {
            curMapMonster = stageManager.monsterData;
        }
     


        if(curMapMonster != null)
        {
            uimanager.stageRoundText.text = "스테이지 " + curMapMonster.stageNum.ToString() + "-" + curMapMonster.roundNum.ToString();
        }
        

    }

    private void OnChangeMoney()
    {
        moneyText.text = GameManager.Instance.gameMoney.ToString();
        gemText.text = GameManager.Instance.gameGem.ToString(); 
    }

    private void OnStageEnd(bool result)
    {
        if (result)
        {
            uimanager.stageResultText.color = Color.green;
            uimanager.stageResultText.text = "스테이지 클리어!!";
        }
        else if(!result)
        {
            uimanager.stageResultText.color = Color.red;
            uimanager.stageResultText.text = "스테이지 실패!";
        }
         
    }
}
