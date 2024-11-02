using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
     public float rotationSpeed = 1000f; // Velocidad de rotación en grados por segundo

    private Vector2 direction;

    // Método para establecer la dirección del proyectil
    public void SetDirection(bool mirandoIzquierda){
        direction = mirandoIzquierda ? Vector2.left : Vector2.right;
    }

    void Start()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        // Asigna la dirección del proyectil
        rb2D.velocity = direction * speed;

        if (SceneManager.GetActiveScene().name == "jefe")
        {
            // Destruye el proyectil después de 3 segundos
            Destroy(gameObject, 9f);
        }
        else
        {
            // Destruye el proyectil después de 0.2 segundos
            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        // Rota el proyectil continuamente en el eje Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("ProjectilePlayer") && collision.gameObject.CompareTag("Player") == true){

        }else{
            if (gameObject.CompareTag("ProjectilePlayer") && collision.gameObject.CompareTag("Player") == false) // No destruir el proyectil si se choca con el que lo tira
            {
                Destroy(gameObject); // Destruye el proyectil al colisionar con algo
            }
            if (gameObject.CompareTag("ProjectileEnemy") && collision.gameObject.CompareTag("Enemy") == true)
            {
                // No hacer nada si colisiona con un enemigo
            }
            else if (gameObject.CompareTag("ProjectileEnemy"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
