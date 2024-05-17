using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmoBehavior : MonoBehaviour
{
    [SerializeField] private CharacterObject characterObject;
    [SerializeField] private Collider soundArea;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        StartCoroutine(MoveInsideArea());
    }

    private IEnumerator MoveInsideArea()
    {
        Vector3 destination = Vector3.zero;

        while (destination == Vector3.zero) 
        { 
            destination = GetRandomPointInArea(soundArea);
            //Debug.Log("Procurando destino"); 
        }

        //Debug.Log("Achou destino");

        agent.SetDestination(destination);

        while (!InDestination()) { yield return null; }

        yield return new WaitForSeconds(2f);

        StartCoroutine(MoveInsideArea());
    }

    private Vector3 GetRandomPointInArea(Collider area)
    {
        Bounds bounds = area.bounds;

        float randX = Random.Range(bounds.min.x, bounds.max.x);
        float randZ = Random.Range(bounds.min.z, bounds.max.z);
        float centerY = bounds.center.y;

        Vector3 randomPoint = new Vector3(randX, centerY, randZ);

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) { return hit.position; }

        return Vector3.zero;
    }

    private bool InDestination()
    {
        // Retorna verdade se distancia que falta for menor que a distancia de parar e se nao ja tiver caminho a percorrer
        return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
    }
}
