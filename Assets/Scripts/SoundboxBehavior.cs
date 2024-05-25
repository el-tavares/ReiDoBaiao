using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboxBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private int index;
    [SerializeField] private GameObject minigame;


    [HideInInspector] public int interactedIndex;

    public void Interact()  // IInteractable
    {
        this.interactedIndex = this.index;
        minigame.SetActive(true); 
        GetComponent<SphereCollider>().enabled = false;       
    }
}
