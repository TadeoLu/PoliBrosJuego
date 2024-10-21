using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 2;
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public bool lado = false; //false mirando derecha, true mirando izquierda  
    public Vector2 spawn;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Shoot();
    }

	private void Move()
	{
	    float horizontalInput = 0;

	    if (Input.GetKey("d"))
	    {
		horizontalInput = runSpeed;
		transform.localScale = new Vector3(1, 1, 1); // Mira a la derecha
		lado=false;
		
	    }
	    else if (Input.GetKey("a"))
	    {
		horizontalInput = -runSpeed;
		transform.localScale = new Vector3(-1, 1, 1); // Mira a la izquierda
		lado=true;
	    }

	    rb2D.velocity = new Vector2(horizontalInput, rb2D.velocity.y);
	}

    private void Jump()
    {
        if (Input.GetKey("w") && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }

private void Shoot()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.tag = "ProjectilePlayer";
        projectile.GetComponent<Projectile>().SetDirection(lado);
    }
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("caida"))
        {
            transform.position = spawn;
        }
        if (collision.gameObject.CompareTag("ProjectileEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
	    SceneManager.LoadScene("crearMapa");        
        }
    }

}

