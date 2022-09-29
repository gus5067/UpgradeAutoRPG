using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class StartButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] auClip;

    private AudioSource curAudio;
    private Button button;
    [SerializeField]
    private Image soundeOption;
    private void Awake()
    {
        curAudio = GetComponent<AudioSource>();
        button = GetComponent<Button>();
    }
    public void OnClickStart()
    {
        StartCoroutine(StartRoutine());
    }

    public void OnPointerEnter()
    {
        curAudio.clip = auClip[1];
        curAudio.Play();
    }

    public void OptionEnter()
    {
        curAudio.clip = auClip[0];
        curAudio.Play();
        soundeOption.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OptionExit()
    {
        Time.timeScale = 1;
        soundeOption.gameObject.SetActive(false);
    }
    IEnumerator StartRoutine()
    {
        button.interactable = false;
        curAudio.clip = auClip[0];
        curAudio.Play();
       
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("UpgradeTest");
    }
}
