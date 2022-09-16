using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    [SerializeField]
    public Transform characterPos;
    [SerializeField]
    private UIControllOnStageScene uiController;

    [SerializeField]
    private GameObject uiImg;
    public void OnClickYesButton()
    {
        GameManager.instance.curPos = characterPos.position;
        GameManager.instance.curRotation = characterPos.rotation;
        Time.timeScale = 1f;
        SceneManager.LoadScene("CharacterTest");
    }

    public void OnClickNoButton()
    {
        StartCoroutine(InteractCoolTime());
        Time.timeScale = 1f;
        uiImg.SetActive(false);
    }

    IEnumerator InteractCoolTime()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        uiController.curStage.isInteract = false;
    }
}
