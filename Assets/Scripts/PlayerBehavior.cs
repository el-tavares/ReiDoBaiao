using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // Public pois sao variaveis temporarias para testes
    public bool preciseMovement = true;

    private void FixedUpdate()
    {
        if (preciseMovement)    // Movimento preciso (sem aceleracao)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else    // Movimento suave (com aceleracao)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(horizontal, 0f, vertical) * speed * 1.3f * Time.deltaTime);
        }
    }
}
