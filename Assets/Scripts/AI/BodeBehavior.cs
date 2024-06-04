using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodeBehavior : MonoBehaviour
{
    public static Action<bool> OnBodeHit;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionRange, speedMultiply, setupTime;

    private NavMeshAgent agent;
    private float initialSpeed;

    private void Start()
    {
        // Inicializa
        agent = GetComponent<NavMeshAgent>();
        initialSpeed = agent.speed;
        StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        // Fica esperando jogador aparecer
        yield return new WaitForSeconds(.1f);

        if (PlayerInRange()) { StartCoroutine(ChasePlayer()); }
        else { StartCoroutine(Idle()); }
    }

    private bool PlayerInRange()
    {
        // Retorna condicao do jogador proximo
        return Vector3.Distance(playerTransform.position, this.transform.position) < detectionRange;
    }

    private IEnumerator ChasePlayer()
    {
        // TOCA ANIMACAO DE PREPARACAO

        yield return new WaitForSeconds(setupTime);
        Vector3 playerDirectionOffset = (playerTransform.position - transform.position).normalized * 2f;
        agent.SetDestination(playerTransform.position + playerDirectionOffset);
        agent.speed = initialSpeed * speedMultiply;

        while (!InDestination())
        {
            // TOCA ANIMACAO DE CORRIDA

            yield return null;
        }

        yield return new WaitForSeconds(setupTime);
        StartCoroutine(ChasePlayer());
    }

    private bool InDestination()
    {
        // Retorna condicao de distancia que falta para parar e se nao tem mais caminho a percorrer
        return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Denuncia que bode bateu
        if (other.CompareTag("Player")) { OnBodeHit?.Invoke(true); }
    }
}
