using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboxBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject minigame;

    private bool bOpenToInteraction = false;

    private void Update()
    {
        if (bOpenToInteraction && Input.GetKeyDown(KeyCode.E)) { Interact(); }
    }

    public void Interact()
    {
        minigame.SetActive(true);     
        GetComponent<SphereCollider>().enabled = false;
        bOpenToInteraction = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jogador esta proximo");
        if (other.CompareTag("Player")) { bOpenToInteraction = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Jogador nao esta mais proximo");
        if (other.CompareTag("Player")) { bOpenToInteraction = false; }
    }
}
