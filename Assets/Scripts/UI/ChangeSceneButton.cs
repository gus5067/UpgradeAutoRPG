using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField]
    private MapData InfiniteMap;

    [SerializeField]
    private MonsterData InfiniteMonster;

    private Button toStageButton;

    private RectTransform curStageButtonPos;
    private Vector2 firstPos;

    private void Awake()
    {
        toStageButton = GetComponent<Button>();
        firstPos = toStageButton.GetComponent<RectTransform>().anchoredPosition;
        curStageButtonPos = toStageButton.GetComponent<RectTransform>();
    }
    public void ButtonFocused()
    {
        StartCoroutine("ButtonMoveRoutine");
    }

    public void ButtonFocusedOut()
    {
        StopCoroutine("ButtonMoveRoutine");
        curStageButtonPos.anchoredPosition = firstPos;
    }
    //코루틴을 string으로 사용해야 스탑코루틴 적용 가능 이외에 방법으로 사용할 시 코루틴 변수를 따로 저장해야함
    IEnumerator ButtonMoveRoutine()
    {
        Debug.Log("코루틴 발동");
        float x = firstPos.x;
        while (curStageButtonPos.anchoredPosition.x <= 85)
        {
            curStageButtonPos.anchoredPosition = new Vector3(x, firstPos.y);
            x += 500 * 0.01f;
            yield return new WaitForSeconds(0.01f); 
        }
    
    }

    public void LoadStageScene()
    {
        GameManager.Instance.curState = GameManager.State.Normal;
        SceneManager.LoadScene("StageSelectTest");
    }
    
    public void LoadInfiniteBattleScene()
    {
        GameManager.Instance.curState = GameManager.State.Infinite;
        GameManager.Instance.curSkyBoxNum = 3;
        GameManager.Instance.curMap = InfiniteMap;
        GameManager.Instance.curStage = InfiniteMonster;
        SceneManager.LoadScene("CharacterTest");

    }
    public void CheatOn()
    {
        Debug.Log("치트키 사용");
        GameManager.Instance.gameMoney += 1000;
        GameManager.Instance.gameGem += 10;
    }
}
