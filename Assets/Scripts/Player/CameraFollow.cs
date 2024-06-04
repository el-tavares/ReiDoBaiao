using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        // Salva a posicao atual menos a posicao do target como offset
        offset = this.transform.position - target.position;   // Subtrai a posicao do target porque talvez esse tenha um offset em relacao ao world root
    }

    private void FixedUpdate()
    {
        // Arrasta a camera para a posicao do alvo com delay de 1/4 segundo
        Vector3 targetPosition = target.position + offset;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, 0.25f);
    }
}
