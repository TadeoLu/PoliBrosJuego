using UnityEngine;
using UnityEngine.SceneManagement;

public class projectileJefe : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private Transform playerTransform;
    private bool isHoming = false;

    // Método para establecer la dirección del proyectil
    public void SetDirection(bool mirandoIzquierda)
    {
        direction = mirandoIzquierda ? Vector2.left : Vector2.right;
        isHoming = false; // Desactiva la teledirección si se establece la dirección manualmente
    }

    void Start()
    {
        // Verifica si estamos en la escena del jefe
        if (SceneManager.GetActiveScene().name == "jefe")
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            isHoming = true;
            Destroy(gameObject, 2.9f);
        }
        else
        {
            // Destruye el proyectil después de 0.3 segundos
            Destroy(gameObject, 0.3f);
        }
    }

    void Update()
    {
        if (isHoming && playerTransform != null)
        {
            // Calcula la dirección hacia el jugador
            direction = (playerTransform.position - transform.position).normalized;
        }

        // Mueve el proyectil en la dirección actual
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.CompareTag("ProjectileEnemy"))
        {
            Destroy(gameObject);
        }
        
        
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
        	Destroy(gameObject);
        }        
    }
}

