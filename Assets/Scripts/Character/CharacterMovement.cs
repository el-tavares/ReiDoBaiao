using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterObject characterObject;
    [SerializeField] float animationRate = .1f;
    [SerializeField] float maxAudioVolume = 1f;

    private NavMeshAgent agent;
    private bool bDead, bPlayAnimation = true;
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

    private IEnumerator PlayAnimation()
    {
        Sprite[] currentAnimation = GetCurrentAnimation();
        int currentFrame = 0;

        // Atualiza o frame da animacao enquanto checa a animacao
        while (bPlayAnimation)
        {
            this.GetComponentInChildren<SpriteRenderer>().sprite = currentAnimation[currentFrame];

            currentAnimation = GetCurrentAnimation();

            // A operacao modulo (%) e usada para "enrolar" o indice de volta para 0 quando ele atinge o final da sequencia de frames da animacao
            currentFrame = (currentFrame + 1) % currentAnimation.Length;

            yield return new WaitForSeconds(animationRate);

            HandleSound(false);
        }
    }

    private Sprite[] GetCurrentAnimation()
    {
        if (bDead) { return characterObject.deathExolosionSprites; }
        if (agent.velocity.x > 0 || agent.velocity.z < 0) { return characterObject.walkRightSprites; }
        else if (agent.velocity.x < 0 || agent.velocity.z > 0) { return characterObject.walkLeftSprites; }
        else { return characterObject.idleSprites; }
    }

    private void HandleSound(bool forceSound)
    {
        audioSource.pitch = Random.Range(.5f, 1.5f);
        if (agent.velocity != Vector3.zero || forceSound) { audioSource.volume = Random.Range(maxAudioVolume - .5f, maxAudioVolume); }
        else { audioSource.volume = 0f; }
    }

    public void StartSpecialAnimation() { StartCoroutine(PlaySpecialAnimation()); }

    private IEnumerator PlaySpecialAnimation()
    {
        bPlayAnimation = false;
        Sprite[] specialAnimation = characterObject.specialSprites;

        for (int i = 0; i < specialAnimation.Length; i++)
        {
            this.GetComponentInChildren<SpriteRenderer>().sprite = specialAnimation[i];
            yield return new WaitForSeconds(animationRate);
            HandleSound(true);
        }

        bPlayAnimation = true;
        StartCoroutine(PlayAnimation());
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
