using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;   // Velocidad de movimiento del personaje
    public float jumpForce = 10f; // Fuerza del salto
    public Transform groundCheck; // Transform para verificar si el personaje está en el suelo
    public float groundCheckRadius = 0.2f; // Radio para la verificación del suelo
    public LayerMask groundLayer; // Capa para identificar el suelo
    public float groundCheckDistance = 0.1f; // Distancia para verificar el suelo

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Verificar si está en el suelo usando Raycast
        isGrounded = IsGrounded();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con algo con tag "Wall", asegurarse de que no pueda atravesarlo
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Aquí se puede ajustar la posición del personaje si es necesario
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Se considera que no está en el suelo si deja de colisionar con un objeto con la etiqueta "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Verifica si el personaje está en el suelo
    private bool IsGrounded()
    {
        // Raycast desde la parte inferior del personaje para verificar si está tocando la parte superior de un bloque
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Dibuja el rayo de verificación del suelo en el Editor para depuración
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance);
    }
}

