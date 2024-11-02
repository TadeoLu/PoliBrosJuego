using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento
    public float jumpForce = 10f;  // Fuerza del salto
    public Transform groundCheck;  // Transform para detectar el suelo
    public LayerMask groundLayer;  // Capa del suelo

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;  // Radio de detección del suelo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckGroundStatus();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckGroundStatus()
    {
        // Verifica si el personaje está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
