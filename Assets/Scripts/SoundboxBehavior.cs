using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboxBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private int index;
    [SerializeField] private GameObject minigame;

    [HideInInspector] public int interactedIndex;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()  // IInteractable
    {
        this.interactedIndex = this.index;
        minigame.SetActive(true); 
        GetComponent<SphereCollider>().enabled = false;       
    }

    public void ChangeAudioVolume(float volumeOffset)
    {
        audioSource.volume += volumeOffset;
        Debug.Log(audioSource.volume);
    }
}
