using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))    // Movimento preciso
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

        
        float horizontal = Input.GetAxis("Horizontal");     // Movimento suave (aceleracao)
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime);  
             
    }
}
