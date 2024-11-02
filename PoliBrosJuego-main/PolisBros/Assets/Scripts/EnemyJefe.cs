using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyJefe : Enemy
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float fireRate = 1f; // Tasa de disparo
    private float nextFireTime = 0f; // Tiempo para el próximo disparo
    public Image barraDeVida;
    public float vidaMaxima; 
    public GameObject laser;
    private Laser laserScript;
    public bool spawningLaser = false;
    public Sprite pruscino;
    public Sprite pruscinoBullet;
    public Sprite barbieri;
    public Sprite barbieriBullet;
    public Sprite marinelli;
    public Sprite marinelliBullet;
    public Sprite marinelliLaser;
    public Sprite barbieriLaser;
    public Sprite pruscinoLaser;
private bool hasWaited = false; // Variable booleana para verificar si ya esperó una vez

    void Start()
    {
        int jefe = PlayerPrefs.GetInt("Jefe", 1); // 1 como valor por defecto si no se encuentra
        Debug.Log("Valor de jefe: " + jefe);
        switch(jefe){
            case 2:
                GetComponent<SpriteRenderer>().sprite = pruscino;
                projectilePrefab.GetComponent<SpriteRenderer>().sprite = pruscinoBullet;
                laser.GetComponent<SpriteRenderer>().sprite = pruscinoLaser;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = barbieri;
                projectilePrefab.GetComponent<SpriteRenderer>().sprite = barbieriBullet;
                laser.GetComponent<SpriteRenderer>().sprite = barbieriLaser;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = marinelli;
                projectilePrefab.GetComponent<SpriteRenderer>().sprite = marinelliBullet;
                laser.GetComponent<SpriteRenderer>().sprite = marinelliLaser;
                break;
        }
        //if jefe 1 skin1...
    }



    protected override void Update()
    {
        barraDeVida.fillAmount = vidas / vidaMaxima;
        base.Update();

    }

protected override void PerformAction()
{
    if (!hasWaited)
    {
        StartCoroutine(PerformActionWithDelay());
    }
    else
    {
        PerformActionImmediately();
    }
}

private IEnumerator PerformActionWithDelay()
{
    // Espera 1 segundo solo la primera vez
    yield return new WaitForSeconds(1f);
    
    hasWaited = true; // Cambia la variable a true para no volver a esperar

    PerformActionImmediately();
}

private void PerformActionImmediately()
{
    // Determina la dirección hacia el jugador
    Vector3 direction = (player.position - transform.position).normalized;

    // Calcula el giro
    if (direction.x > 0)
    {
        Flip(false); // Gira a la derecha
        Shoot(false); // Dispara hacia la derecha
    }
    else
    {
        Flip(true); // Gira a la izquierda
        Shoot(true); // Dispara hacia la izquierda
    }
}


    private void Shoot(bool left)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.tag = "ProjectileEnemy";
            projectile.GetComponent<Projectile>().SetDirection(left); // Envía la dirección al proyectil
        }
    }
        protected override void verificarVidas(){
        Debug.Log("Papi"  + (vidas<=vidaMaxima/2));
        if (vidas<=vidaMaxima/2 && spawningLaser == false)
        {
            spawningLaser = true;
            laserScript = laser.GetComponent<Laser>();
            StartCoroutine(SpawnLaserLoop());
        }
        if(vidas <= 0){
            Destroy(gameObject);
            SceneManager.LoadScene("ganaste");        
        }
    }
      IEnumerator SpawnLaserLoop()
    {
        while(true){
            yield return StartCoroutine(laserScript.SpawnLaser());
        }
    }
}
