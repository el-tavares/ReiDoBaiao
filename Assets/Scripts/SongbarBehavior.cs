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
    private bool bHit;

    private void FixedUpdate()
    {
        // Anda pra direita se direcao for 1 e valor da barra nao chega em 1. Anda pra esquerda se direcao for -1 e valor da barra nao chega em 0
        if (handlebar.value < 1f && direction > 0) { handlebar.value += speed * direction * Time.deltaTime; }
        else if (handlebar.value > 0f && direction < 0) { handlebar.value += speed * direction * Time.deltaTime; }
        else { direction *= -1f; }  // Troca de direcao quado a barra atingir os valores
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bHit)  // Checa se colidiu e a barra de espaco esta pressionado enquanto nao acerta
        {
            if (other.CompareTag("Hit") && Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Trigger");
                bHit = true;    // Fecha a checagem para garantir que rode esse metodo uma vez
            }
        }      
    }
}
