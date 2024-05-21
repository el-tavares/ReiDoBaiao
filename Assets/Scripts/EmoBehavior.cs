using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmoBehavior : MonoBehaviour
{
    [SerializeField] private Collider soundArea;

    private NavMeshAgent agent;
    private Transform player;
    private float fleeDistance;
    private bool bFleeing;

    private Animator animator;

    private void Update()
    {
        if ( agent.velocity.x > 0 )
        {
            Debug.Log("Direita");
        }
        else if (agent.velocity.x < 0)
        {
            Debug.Log("Esquerda");
        }
    }

    private void Start()
    {
        // Defini valores iniciais
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(MoveInsideArea());
    }

    private IEnumerator MoveInsideArea()
    {
        Vector3 destination = Vector3.zero;

        // Procura novo destino diferente de 0
        while (destination == Vector3.zero)
        {
            Debug.Log("Emo procurando destino");
            destination = GetRandomPointInArea(soundArea);
        }

        agent.SetDestination(destination);

        while (!InDestination()) { yield return null; }

        yield return new WaitForSeconds(2f);

        if (!bFleeing) { StartCoroutine(MoveInsideArea()); }
    }

    private Vector3 GetRandomPointInArea(Collider area)
    {
        Bounds bounds = area.bounds;

        // Defini valores randomicos entre as extremidades da area
        float randX = Random.Range(bounds.min.x, bounds.max.x);
        float randZ = Random.Range(bounds.min.z, bounds.max.z);
        float centerY = bounds.center.y;

        Vector3 randomPoint = new Vector3(randX, centerY, randZ);

        // Retorna a posicao se for dentro do navmesh
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) { return hit.position; }

        return Vector3.zero;
    }

    private bool InDestination()
    {
        // Retorna verdade se distancia que falta for menor que a distancia de parar e se nao ja tiver caminho a percorrer
        return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
    }

    private void OnEnable()
    {
        SoundbarBehavior.OnHit += Flee;
    }

    private void OnDisable()
    {
        SoundbarBehavior.OnHit -= Flee;
    }

    private void Flee(int hitCount)
    {   
        bFleeing = true;
        fleeDistance = hitCount;
        StartCoroutine(FleeFromPlayer());
    }

    private IEnumerator FleeFromPlayer()
    {
        while (bFleeing)
        {
            Vector3 destination;

            float distance = Vector3.Distance(this.transform.position, player.position);

            // Define destino de fuga se tiver proximo do jogador
            if (distance < fleeDistance / 2f) 
            { 
                destination = GetFleePoint(player.position, fleeDistance); 
                Debug.Log("Emo fugindo"); 
            }
            else 
            { 
                destination = GetRandomPointInArea(soundArea); 
                Debug.Log("Emo voltando"); 
            }

            agent.SetDestination(destination);

            while (!InDestination()) { yield return null; }

            yield return new WaitForSeconds(1f);
        }
    }

    private Vector3 GetFleePoint(Vector3 fleeSource, float fleeDistance)
    {
        // Defini direcao oposta ao jogador e a posicao de fuga
        Vector3 fleeDirection = (this.transform.position - fleeSource).normalized;
        Vector3 fleePosition = this.transform.position + fleeDirection;

        // Retorna a posicao de fuga se for dentro do navmesh
        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas)) { return hit.position; }

        return this.transform.position;
    }
}
