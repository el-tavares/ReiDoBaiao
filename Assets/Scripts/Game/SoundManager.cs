using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource dynamicAudioSource;

    public void PlaySound(AudioClip audioClip)
    {
        dynamicAudioSource.clip = audioClip;
        dynamicAudioSource.Play();
    }
}
