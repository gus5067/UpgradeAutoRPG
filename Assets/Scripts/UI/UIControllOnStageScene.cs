using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIControllOnStageScene : MonoBehaviour
{
    public StageMonster curStage = null;

    private StageMonster preStage = null;

    [SerializeField]
    private GameObject uiImg;

    [SerializeField]
    private TextMeshProUGUI stageText;


    private void Update()
    {
        StageChange();
    }
    private void StageChange()
    {
        if(curStage != null && preStage == null)
        {
            preStage = curStage;
            curStage.onStageMonster += UIactive;
        }
        else if(preStage != curStage)
        {
            preStage.onStageMonster -= UIactive;
            curStage.onStageMonster += UIactive;
        }
        else
        {
            return;
        }
    }
    private void UIactive(StageMonster curMonster)
    {
        curMonster.isInteract = true;
        uiImg.SetActive(true);
        stageText.text = "Stage " + curMonster.ownMonsterData.stageNum.ToString()+ "-" + curMonster.ownMonsterData.roundNum.ToString();
    }
}
