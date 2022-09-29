using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Image SoundOption;

    public AudioMixer audioMixer;

    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider voiceSlider;

    private void Start()
    {
        StartVolumeSlider();
    }
    public void StartVolumeSlider()
    {
        bgmSlider.value = GameManager.Instance.curBGM;
        sfxSlider.value = GameManager.Instance.curSFX;
        voiceSlider.value = GameManager.Instance.curVoice;
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        GameManager.Instance.curBGM = bgmSlider.value;
    }
    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
        GameManager.Instance.curSFX = sfxSlider.value;
    }
    public void SetVoiceVolume()
    {
        audioMixer.SetFloat("Voice", Mathf.Log10(voiceSlider.value) * 20);
        GameManager.Instance.curVoice = voiceSlider.value;
    }


    public void EnterOptionButton()
    {
        SoundOption.transform.SetAsLastSibling();
        SoundOption.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitOptionButton()
    {
        Time.timeScale = 1;
        SoundOption.gameObject.SetActive(false);
    }
}
