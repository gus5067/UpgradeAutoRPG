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

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
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

    public void OnClickBackButton()
    {
        uiController.CurPlayerPos();
        StartCoroutine(BackToUpgradeScene());
    }

    public void OnPointerEnter()
    {
        audioManager.PlayerEffectSound(audioManager.audioClips[0]);
    }
    IEnumerator InteractCoolTime()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        uiController.curStage.isInteract = false;
    }

    IEnumerator BackToUpgradeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("UpgradeTest");
    }
}
