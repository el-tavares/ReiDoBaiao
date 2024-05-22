using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private NavMeshAgent agent;
    private Collider other;

    private void Start()
    {
        // Defini agent navmesh
        agent = GetComponent<NavMeshAgent>();        
    }

    private void Update()
    {
        // Interage se existe outro objeto e pressionou 'E'
        if (other != null && Input.GetKeyUp(KeyCode.E))
        {
            other.gameObject.GetComponent<IInteractable>().Interact();
            other = null;
        }

        IsObjectInFront();
    }

    private void FixedUpdate()
    {
        // Seta destino do agent com base na direcao do input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 position = this.transform.position + direction;   
        
        agent.SetDestination(position);
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

    private bool IsObjectInFront()
    {
        Debug.DrawRay(cam.position, this.transform.position - cam.position, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(cam.position, this.transform.position - cam.position, out hit))
        {
            if (hit.collider.CompareTag("Hideable")) { return true; }
        }
        return false;
    }
}
