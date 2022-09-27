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
    IEnumerator StartRoutine()
    {
        button.interactable = false;
        curAudio.clip = auClip[0];
        curAudio.Play();
       
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("UpgradeTest");
    }
}
