using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StageMonster : MonoBehaviour,IInteractable
{
    [SerializeField]
    private UIControllOnStageScene uiController;
    public MonsterData ownMonsterData;

    public MapData ownMapData;

    public bool isInteract;

    public event UnityAction<StageMonster> onStageMonster;

    public void Interact()
    {
        if (!isInteract)
        {
            if (ownMonsterData != null)
            {
                uiController.curStage = this;
                if (ownMapData != null)
                {
                    GameManager.Instance.curMap = ownMapData;
                }
                GameManager.Instance.curStage = ownMonsterData;
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
