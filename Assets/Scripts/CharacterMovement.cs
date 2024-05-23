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

    private void Start()
    {
        // Defini agent e desabilita rotacao
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agent.updateRotation = false; }

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

            if (currentFrame < currentAnimation.Length - 1) { currentFrame++; }
            else { currentFrame = 0; }

            yield return new WaitForSeconds(animationRate);
        }
    }

    private Sprite[] GetCurrentAnimation()
    {
        if (bDead) { return characterObject.deathExolosionSprites; }
        if (agent.velocity.x > 0 || agent.velocity.z < 0) { return characterObject.walkRightSprites; }
        else if (agent.velocity.x < 0 || agent.velocity.z > 0) { return characterObject.walkLeftSprites; }
        else { return characterObject.idleSprites; }
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
