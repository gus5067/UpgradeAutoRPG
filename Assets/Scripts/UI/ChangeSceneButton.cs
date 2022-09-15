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
        Debug.Log("��ư ���� ����");
        StartCoroutine("ButtonMoveRoutine");
    }

    public void ButtonFocusedOut()
    {
        Debug.Log("��ư ����");
        StopCoroutine("ButtonMoveRoutine");
        curStageButtonPos.anchoredPosition = firstPos;
    }
    //�ڷ�ƾ�� string���� ����ؾ� ��ž�ڷ�ƾ ���� ���� �̿ܿ� ������� ����� �� �ڷ�ƾ ������ ���� �����ؾ���
    IEnumerator ButtonMoveRoutine()
    {
        Debug.Log("�ڷ�ƾ �ߵ�");
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
