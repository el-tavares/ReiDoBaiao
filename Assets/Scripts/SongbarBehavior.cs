using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongbarBehavior : MonoBehaviour
{
    [SerializeField] private Scrollbar handlebar;
    [SerializeField] private Scrollbar hitbar;
    [SerializeField] private float speed = 1f;

    private float direction = 1f;
    private bool bPressing;
    private bool bWasHit;
    private bool bOpenInput = true;

    private void Start()
    {
        hitbar.value = Random.Range(0f, 1f);    // Escolhe posicao randomica do hitbar
    }

    private void Update()
    {
        // Anda pra direita se direcao for 1 e valor da barra nao chega em 1. Anda pra esquerda se direcao for -1 e valor da barra nao chega em 0
        if (handlebar.value < 1f && direction > 0) { handlebar.value += speed * direction * Time.deltaTime; }
        else if (handlebar.value > 0f && direction < 0) { handlebar.value += speed * direction * Time.deltaTime; }
        else { direction *= -1f; }  // Troca de direcao quado a barra atingir os valores

        if (!bWasHit && bOpenInput)   // Checa se barra de espaco for pressionado ou nao se nao tiver acertado e disponivel
        {
            if (Input.GetKey(KeyCode.Space)) { bPressing = true; }  
            else { bPressing = false; }
        }    

        if (Input.GetKeyUp(KeyCode.Space)) { bOpenInput = true; }  // Torna disponivel depois que solta a barra de espaco
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bWasHit && bOpenInput)  // Checa se colidiu e a barra de espaco esta pressionado enquanto nao acerta
        {
            if (other.CompareTag("Hit") && bPressing)
            {   
                Debug.Log("Acertou");
                hitbar.value = Random.Range(0f, 1f);    // Escolhe posicao randomica do hitbar
                bOpenInput = false;                        // Fecha disponibilidade
                bWasHit = true;                            // Fecha a checagem para garantir que rode esse metodo uma vez
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (bWasHit) { bWasHit = false; }     // Reinicia hit como nao acertado    
    }
}
