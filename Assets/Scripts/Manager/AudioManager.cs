using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField, Header("È¿°úÀ½")]
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayerEffectSound(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayerEffectLoop(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }
}
