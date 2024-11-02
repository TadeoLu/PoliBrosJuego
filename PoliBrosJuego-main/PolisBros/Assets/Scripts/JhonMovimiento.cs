using UnityEngine;

public class JhonMovimiento : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider; // Cambiado a CapsuleCollider2D
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>(); // Cambiado a CapsuleCollider2D
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, groundLayer); // Cambiado a CapsuleCast
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Vertical, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // Cambiado a CapsuleCast
        return raycastHit.collider != null;
    }
}

