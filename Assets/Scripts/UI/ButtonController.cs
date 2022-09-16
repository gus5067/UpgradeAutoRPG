using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private UIControllOnStageScene uiController;

    [SerializeField]
    private GameObject uiImg;
    public void OnClickYesButton()
    {
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
