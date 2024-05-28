using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmoBehavior : MonoBehaviour
{
    public static Action OnEmoExplosion;

    [SerializeField] private Transform player;
    [SerializeField] private SoundboxBehavior soundbox;
    [SerializeField] private int groupIndex;
    [SerializeField] private GameObject morteAudio;

    private NavMeshAgent agent;
    private float fleeDistance;
    private bool bFleeing;
    private CharacterMovement characterMovement;

    private void Start()
    {
        // Defini valores iniciais
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        characterMovement = GetComponent<CharacterMovement>();

        StartCoroutine(MoveInsideArea());
    }

    private IEnumerator MoveInsideArea()
    {
        while (!bFleeing)
        {           
            agent.SetDestination(GetRandomPointInArea(soundbox.gameObject.transform.position, 5f, false));

            while (!InDestination()) { yield return null; }
            
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector3 GetRandomPointInArea(Vector3 position, float offset, bool oposite)
    {
        // Troca a direcao e velocidade se estiver fugindo
        if (oposite)
        {          
            Vector3 opositeDirection = (this.transform.position - position).normalized;
            position = this.transform.position + opositeDirection * offset;
            agent.speed = 4f;
        }
        else { agent.speed = 2f; }

        Vector3 randomPoint = GetRandomPoint(position, offset);
        NavMeshHit hit;    
        
        // Procura posicao dentro do navmesh
        while (!NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas)) 
        {
            randomPoint = GetRandomPoint(position, offset);
        }

        return hit.position; 
    }

    private Vector3 GetRandomPoint(Vector3 position, float offset)
    {
        // Retorna ponto randomico dentro dos limites
        float randX = UnityEngine.Random.Range(position.x - offset, position.x + offset);
        float randZ = UnityEngine.Random.Range(position.z - offset, position.z + offset);

        return new Vector3(randX, 0f, randZ);
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
        // Foge ate distancia baseada no contador de acertos se for do mesmo time e index do som     
        if (this.groupIndex == soundbox.interactedIndex)
        {
            Debug.Log($"Emo do grupo {this.groupIndex} fugindo. Caixa de som: {soundbox.interactedIndex}");
            bFleeing = true;
            fleeDistance = hitCount;
            soundbox.ChangeAudioVolume(-0.07f);

            if (hitCount > 4) 
            {
                morteAudio.SetActive(true);
                soundbox.ChangeAudioVolume(-1f);
                OnEmoExplosion?.Invoke();
                characterMovement.SetDead(); 
            }

            StartCoroutine(FleeFromPlayer());
        }
    }

    private IEnumerator FleeFromPlayer()
    {
        while (bFleeing)
        {
            Vector3 destination;
            float distance = Vector3.Distance(this.transform.position, player.position);

            // Define destino de fuga se tiver proximo do jogador
            if (distance < 2f * fleeDistance) { destination = GetRandomPointInArea(player.position, 1.5f * fleeDistance, true); }
            else { destination = GetRandomPointInArea(soundbox.gameObject.transform.position, 2.5f * fleeDistance, false); }

            agent.SetDestination(destination);
            
            while (!InDestination()) { yield return null; }

            yield return new WaitForSeconds(1f);
        }
    }
}
