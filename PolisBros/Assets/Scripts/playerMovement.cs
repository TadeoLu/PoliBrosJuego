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
    public int vidas = 3;
    private List<GameObject> instanciasVidas = new List<GameObject>();
    public GameObject laser;
    public GameObject vidaPrefab;

    public bool iframes = false;
        private Renderer renderer; // Para acceder al componente Renderer del personaje
    private Color originalColor;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "jefe"){
            GenerarVidas();
        }
        rb2D = GetComponent<Rigidbody2D>();
        // Obtén el Renderer del GameObject
        renderer = GetComponent<Renderer>();
        // Guarda el color original
        originalColor = renderer.material.color;
    }

    void GenerarVidas(){
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.365f, 1.15f, 0), Quaternion.identity));
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.24f, 1.15f, 0), Quaternion.identity));
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.119f, 1.15f, 0), Quaternion.identity));

    }
    void FixedUpdate()
    {
        Move();
        Jump();
        Shoot();
    }
    protected void bajarVida(){
        if (!iframes)
        {
            vidas -= 1;
            instanciasVidas[vidas].GetComponent<vida>().borrarVida();
            verificarVidas();
            iframes=true;    
            StartCoroutine(ResetIframes());
        }

        
    }

    protected void verificarVidas(){
        if(vidas <= 0){
	        SceneManager.LoadScene("jefe");        
        }
    }

    private IEnumerator ResetIframes()
    {
        // Parpadeo durante 1 segundo
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            // Alterna entre el color original y un color más oscuro
            renderer.material.color = Color.Lerp(originalColor, Color.gray, Mathf.PingPong(elapsedTime * 10, 1));
            elapsedTime += Time.deltaTime;
            yield return null; // Espera un frame
        }

        // Restablece el color original
        renderer.material.color = originalColor;
        // Reinicia iframes
        iframes = false;
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
            if (SceneManager.GetActiveScene().name == "jefe")
            {
                bajarVida();
            }else
            {
	            SceneManager.LoadScene("crearMapa");        
            }
        }
        
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("ProjectileEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
            if (SceneManager.GetActiveScene().name == "jefe")
            {
                bajarVida();
            }else
            {
	            SceneManager.LoadScene("crearMapa");        
            }        
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            Laser laserScript = collision.gameObject.GetComponent<Laser>();
            if (laserScript.laserActivo)
            {
                bajarVida();
            } 
        }
    }


}

