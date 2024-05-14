using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool preciseMovement = true;

    [SerializeField] private float speed = 1f;

    private Collider other;

    private void FixedUpdate()
    {
        if (preciseMovement)    // Movimento preciso (sem aceleracao)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else    // Movimento suave (com aceleracao)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(horizontal, 0f, vertical) * speed * 1.3f * Time.deltaTime);
        }
    }

    private void Update()
    {
        // Interage se existe outro objeto e pressionou 'E'
        if (other != null && Input.GetKeyUp(KeyCode.E))
        {
            other.gameObject.GetComponent<IInteractable>().Interact();
            other = null;
        }        
    }

    private void OnTriggerEnter(Collider _other)
    {
        // Salva o outro objeto interagivel
        if (_other.CompareTag("Interactable"))
        {
            Debug.Log("Jogador entrou");
            other = _other;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Descarta o objeto interagivel
        if (_other.CompareTag("Interactable"))
        {
            Debug.Log("Jogador saiu");
            other = null;
        }
    }
}
