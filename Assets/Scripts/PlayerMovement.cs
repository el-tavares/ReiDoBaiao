using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterObject characterObject;
    [SerializeField] float animationRate = .04f;

    private NavMeshAgent agent;
    private Vector3 velocity;
    private Vector3 previousPosition;

    private void Start()
    {
        // Defini valores iniciais
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agent.updateRotation = false; } 

        previousPosition = this.transform.position;

        StartCoroutine(PlayAnimation());
    }

    private void Update()
    {
        velocity = (this.transform.position - previousPosition) / Time.deltaTime;
        previousPosition = this.transform.position;
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
        if (agent != null)
        {
            if (velocity.x > 0 || velocity.y > 0) { return characterObject.walkRightSprites; }
            else if (velocity.x < 0 || velocity.y < 0) { return characterObject.walkLeftSprites; }
            else { return characterObject.idleSprites; }
        }
        else
        {
            if (agent.velocity.x > 0 || velocity.y > 0) { return characterObject.walkRightSprites; }
            else if (agent.velocity.x < 0 || velocity.y < 0) { return characterObject.walkLeftSprites; }
            else { return characterObject.idleSprites; }
        }
    }
}
