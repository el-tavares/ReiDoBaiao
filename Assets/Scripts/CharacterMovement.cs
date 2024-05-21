using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterObject characterObject;
    [SerializeField] float animationRate = .04f;

    private NavMeshAgent agent;

    private void Start()
    {
        // Defini valores iniciais
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        Sprite[] currentAnimation = GetCurrentAnimation();

        for (int i = 0; i < currentAnimation.Length; i++)
        {
            this.GetComponentInChildren<SpriteRenderer>().sprite = currentAnimation[i];

            currentAnimation = GetCurrentAnimation();

            yield return new WaitForSeconds(animationRate);
        }

        StartCoroutine(PlayAnimation());
    }

    private Sprite[] GetCurrentAnimation()
    {
        if (agent.velocity.x > 0) { return characterObject.walkRightSprites; }
        else if (agent.velocity.x < 0) { return characterObject.walkLeftSprites; }
        else { return characterObject.idleSprites; }
    }
}
