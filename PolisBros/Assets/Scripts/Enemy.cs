using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 1f; // Rango de detección
    public Transform player; // Referencia al jugador
    public int vidas;
    
    protected virtual void Update()
    {
        DetectPlayer();
    }

    protected void bajarVida(){
        vidas -= 1;
        verificarVidas();
    }

    protected void verificarVidas(){
        if(vidas <= 0){
            Destroy(gameObject);
        }
    }

    protected void DetectPlayer()
    {
        // Verifica la distancia entre el enemigo y el jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            PerformAction();
        }
    }

    protected virtual void PerformAction()
    {
        // Acción a realizar cuando se detecta al jugador
    }

    protected void Flip(bool facingRight)
    {
        // Gira el enemigo dependiendo de la dirección del jugador
        if (facingRight && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (!facingRight && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ProjectilePlayer"))
        {
            bajarVida();
        }
    }
}
