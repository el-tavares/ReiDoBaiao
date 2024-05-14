using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboxBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject minigame;

    public void Interact()  // IInteractable
    {
        Debug.Log("Minigame comecou");
        minigame.SetActive(true); 
        GetComponent<SphereCollider>().enabled = false;       
    }
}
