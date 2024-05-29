using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehavior : MonoBehaviour
{
    [SerializeField] private Text tutorial;

    private int interactionCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && interactionCount < 1) 
        { 
            tutorial.gameObject.SetActive(true); 
            interactionCount++;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { tutorial.gameObject.SetActive(false); }
        //Destroy(this);
    }
}
