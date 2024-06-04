using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterObject characterObject;
    [SerializeField] float animationRate = .1f;

    private NavMeshAgent agent;
    private bool bDead;
    private AudioSource audioSource;

    private void Start()
    {
        // Defini agent e desabilita rotacao
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agent.updateRotation = false; }

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) { audioSource.Play(); }

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        Sprite[] currentAnimation = GetCurrentAnimation();
        int currentFrame = 0;

        // Atualiza o frame da animacao enquanto checa a animacao
        while (true)
        {
            this.GetComponentInChildren<SpriteRenderer>().sprite = currentAnimation[currentFrame];

            currentAnimation = GetCurrentAnimation();

            // A operacao modulo (%) e usada para "enrolar" o indice de volta para 0 quando ele atinge o final da sequencia de frames da animacao
            currentFrame = (currentFrame + 1) % currentAnimation.Length;

            yield return new WaitForSeconds(animationRate);

            HandleSound();
        }
    }

    private Sprite[] GetCurrentAnimation()
    {
        if (bDead) { return characterObject.deathExolosionSprites; }
        if (agent.velocity.x > 0 || agent.velocity.z < 0) { return characterObject.walkRightSprites; }
        else if (agent.velocity.x < 0 || agent.velocity.z > 0) { return characterObject.walkLeftSprites; }
        else { return characterObject.idleSprites; }
    }

    private void HandleSound()
    {
        audioSource.pitch = Random.Range(.5f, 1.5f);
        if (agent.velocity != Vector3.zero) { audioSource.volume = Random.Range(.5f, 1f); }
        else { audioSource.volume = 0f; }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetDead()
    { 
        bDead = true; 
        agent.isStopped = true;
        Invoke("Die", 1f);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
