using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;

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
            Destroy(gameObject, 3f);
        }
        else
        {
            // Destruye el proyectil después de 0.3 segundos
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    	if(gameObject.CompareTag("ProjectilePlayer") && collision.gameObject.CompareTag("Player") == false){//No destruir el projectile si se choca con el que lo tira
        Destroy(gameObject); // Destruye el proyectil al colisionar con algo
        }
        if(gameObject.CompareTag("ProjectileEnemy")){
        	Destroy(gameObject);
        }
    }
}
