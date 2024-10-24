using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;
    public LayerMask groundLayer; // Capa que define qué objetos se consideran suelo.

    void Start(){
        Debug.Log("epmpezo");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Laser")))
        {
            isGrounded = true;
            Debug.Log(isGrounded);    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Laser")))
        {
            isGrounded = true;
            Debug.Log(isGrounded);    
        }
            

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Laser")))
        {
            isGrounded = false;
            Debug.Log(isGrounded);    
        }

    }
}
