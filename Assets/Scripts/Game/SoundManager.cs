using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource dynamicAudioSource;
    public bool shouldCheckForEnd;

    private void Start()
    {
        if (shouldCheckForEnd) { StartCoroutine(CheckForEnd()); }
    }

    public void PlaySound(AudioClip audioClip)
    {
        dynamicAudioSource.clip = audioClip;
        dynamicAudioSource.Play();
    }

    private IEnumerator CheckForEnd()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        while (!IsAudioEnd(audioSource)) { yield return new WaitForSeconds(.1f); }

        SceneManager.LoadScene("GameScene");
    }

    private bool IsAudioEnd(AudioSource audioSource)
    {
        return audioSource != null && !audioSource.isPlaying;
    }
}
