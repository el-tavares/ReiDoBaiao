using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundboxBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private int index;
    [SerializeField] private GameObject minigame;
    [SerializeField] private Text tutorial;

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
        
        if (index == 1)
        {
            tutorial.gameObject.SetActive(true);
            Invoke("DisableTutorial", 2);
        }
    }

    public void ChangeAudioVolume(float volumeOffset)
    {
        audioSource.volume += volumeOffset;
        Debug.Log(audioSource.volume);
    }

    private void DisableTutorial() { tutorial.gameObject.SetActive(false);}
}
