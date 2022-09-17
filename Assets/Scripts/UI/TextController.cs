using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{

    private UIManager uimanager;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private TextMeshProUGUI gemText;

    private TextMeshProUGUI stageEndText;

    private MonsterData curMapMonster;
    private void Awake()
    {
        uimanager = FindObjectOfType<UIManager>();
        GameManager.Instance.onChangeMoney += OnChangeMoney;
        StageManager.onStageEnd += OnStageEnd;
    }
    private void Start()
    {
        moneyText.text = GameManager.Instance.gameMoney.ToString();
        gemText.text = GameManager.Instance.gameGem.ToString();
        curMapMonster = StageManager.monsterData;


        if(curMapMonster != null)
        {
            uimanager.stageRoundText.text = "�������� " + curMapMonster.stageNum.ToString() + "-" + curMapMonster.roundNum.ToString();
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
            uimanager.stageResultText.text = "�������� Ŭ����!!";
        }
        else if(!result)
        {
            uimanager.stageResultText.color = Color.red;
            uimanager.stageResultText.text = "�������� ����!";
        }
         
    }
}
