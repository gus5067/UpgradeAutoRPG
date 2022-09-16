using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StageMonster : MonoBehaviour,IInteractable
{
    [SerializeField]
    private UIControllOnStageScene uiController;
    public MonsterData ownMonsterData;

    public bool isInteract;

    public event UnityAction<StageMonster> onStageMonster;

    public void Interact()
    {
        if(!isInteract)
        {
            if (ownMonsterData != null)
            {
                uiController.curStage = this;
                GameManager.instance.curStage = ownMonsterData;
                onStageMonster?.Invoke(this);
                Time.timeScale = 0f;
            }
        }
        else
        {
            return;
        }
      

    }

}
