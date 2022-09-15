using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{

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
        Debug.Log("버튼 위에 있음");
        StartCoroutine("ButtonMoveRoutine");
    }

    public void ButtonFocusedOut()
    {
        Debug.Log("버튼 나감");
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
        SceneManager.LoadScene("StageSelectTest");
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("CharacterTest");
    }
}
