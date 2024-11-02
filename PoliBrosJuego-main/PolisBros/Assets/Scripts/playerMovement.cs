using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerMovement : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 2;
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public bool lado = false; //false mirando derecha, true mirando izquierda  
    public Vector2 spawn;
    private Rigidbody2D rb2D;
    public int vidas = 1;
    private List<GameObject> instanciasVidas = new List<GameObject>();
    public GameObject laser;
    public GameObject vidaPrefab;
    public EventSystem eventSystem;
    public GameObject canvas; 
    public bool iframes = false;
    private Renderer renderer; // Para acceder al componente Renderer del personaje
    private Color originalColor;
    private bool muerto = false; // Nueva variable para comprobar si el personaje ha muerto
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name == "jefe"){
            GenerarVidas();
        }
        rb2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    void GenerarVidas(){
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.365f, 1.15f, 0), Quaternion.identity));
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.24f, 1.15f, 0), Quaternion.identity));
        instanciasVidas.Add(Instantiate(vidaPrefab, new Vector3(-1.119f, 1.15f, 0), Quaternion.identity));
    }

    void FixedUpdate()
    {
        if (!muerto) // Solo permite movimiento y disparo si el personaje no ha muerto
        {
            Move();
            Jump();
            Shoot();
        }
    }
    void Update()
    {

         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);

        }
    }
    

    protected void bajarVida()
    {
        if (!iframes && !muerto) // Solo se baja vida si no está en iframes ni ha muerto
        {
            vidas -= 1;
            if (SceneManager.GetActiveScene().name == "jefe"){
                instanciasVidas[vidas].GetComponent<vida>().borrarVida();
            }
            verificarVidas();
            iframes = true;
            StartCoroutine(ResetIframes());
        }
    }

    protected void verificarVidas()
    {
        if (vidas <= 0 && !muerto) // Solo ejecuta si no ha muerto aún
        {
            muerto = true;
            StartCoroutine(murio());
        }
    }

    IEnumerator murio()
    {
        rb2D.velocity = Vector2.zero; // Detiene cualquier movimiento
        rb2D.simulated = false; // Desactiva la física
        transform.Rotate(0, 0, 90); // Rota el personaje 90 grados
        yield return new WaitForSeconds(2); 
        
        GameObject canvasObject = Instantiate(canvas, new Vector3(0, 0, 0), Quaternion.identity);
        canvasObject.GetComponent<Navegator>().eventSystem = eventSystem;
    }

    private IEnumerator ResetIframes()
    {
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(originalColor, Color.gray, Mathf.PingPong(elapsedTime * 10, 1));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = originalColor;
        iframes = false;
    }

    private void Move()
    {
        float horizontalInput = 0;

        if (Input.GetKey("d"))
        {
            horizontalInput = runSpeed;
            transform.localScale = new Vector3(0.1159393f, 0.1159393f, 1);
            lado = false;
        }
        else if (Input.GetKey("a"))
        {
            horizontalInput = -runSpeed;
            transform.localScale = new Vector3(-0.1159393f, 0.1159393f, 1);
            lado = true;
        }

        rb2D.velocity = new Vector2(horizontalInput, rb2D.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey("p") && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }

    private void Shoot()
    {
        if (Input.GetKey("o"))
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.tag = "ProjectilePlayer";
            projectile.GetComponent<Projectile>().SetDirection(lado);
            projectile.GetComponent<Projectile>().speed = 3fs;
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
            bajarVida();
        }
        if (collision.gameObject.CompareTag("jefe"))
        {
            vidas=0;
            verificarVidas();
        }
        
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("ProjectileEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
            bajarVida();
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            Laser laserScript = collision.gameObject.GetComponent<Laser>();
            if (laserScript.laserActivo)
            {
                bajarVida();
            } 
        }
                if (collision.gameObject.CompareTag("jefe"))
        {
            vidas=0;
            verificarVidas();
        }
    }

    public void reintentar()
    {
        if (SceneManager.GetActiveScene().name == "jefe")
        {
            SceneManager.LoadScene("jefe");
        }
        else if (SceneManager.GetActiveScene().name == "crearMapa")
        {
            SceneManager.LoadScene("crearMapa");
        }
        else if (SceneManager.GetActiveScene().name == "historia")
        {
            SceneManager.LoadScene("historia");
        }
    }

    public void salir()
    {
        SceneManager.LoadScene("menu");
    }

    public void jugarBTN()
    {
        SceneManager.LoadScene("elegirModo");
    }

    public void jugarPropioBTN()
    {
        SceneManager.LoadScene("jugar");
    }
        public void historiaBTN()
    {
        DataManager.Instance.usuarioId = "877438172072";
        SceneManager.LoadScene("historia");
    }
        public void jugarJefe()
    {
        SceneManager.LoadScene("jefe");
    }
}