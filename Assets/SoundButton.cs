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

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }
    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }
    public void SetVoiceVolume()
    {
        audioMixer.SetFloat("Voice", Mathf.Log10(voiceSlider.value) * 20);
    }


    public void EnterOptionButton()
    {
        SoundOption.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitOptionButton()
    {
        Time.timeScale = 1;
        SoundOption.gameObject.SetActive(false);
    }
}
