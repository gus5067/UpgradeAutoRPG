using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerWeaponSounds : MonoBehaviour
{
    private AudioSource curAudio;
    [SerializeField]
    private AudioClip[] clips;
    private Player player;


    private void Awake()
    {
        curAudio = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        player.onWeaponSound += WeaponSound;
    }

    public void WeaponSound()
    {
        curAudio.clip = clips[Random.Range(0, clips.Length)];
        curAudio.Play();
    }
}
