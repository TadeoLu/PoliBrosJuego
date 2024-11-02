using UnityEngine;
using System.Collections; 

public class EnemyShooter : Enemy
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float fireRate = 1f; // Tasa de disparo
    public float initialDelay = 1f; // Retardo inicial para el primer disparo
    private float nextFireTime = 0f; // Tiempo para el próximo disparo
    private bool firstShot = true; // Control para el primer disparo

    public Sprite normalSprite; // Sprite cuando no está disparando
    public Sprite shootingSprite; // Sprite cuando está disparando
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private float shootDuration = 0.2f; // Duración para mantener el sprite de disparo

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Establece el tiempo de la primera espera para el disparo inicial
        nextFireTime = Time.time + initialDelay;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void PerformAction()
    {
        // Determina la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Calcula el giro
        if (direction.x > 0)
        {
            Flip(true); // Gira a la derecha
            Shoot(false); // Dispara hacia la derecha
        }
        else
        {
            Flip(false); // Gira a la izquierda
            Shoot(true); // Dispara hacia la izquierda
        }
    }

    private void Shoot(bool left)
    {
        if (Time.time >= nextFireTime)
        {
            if (firstShot)
            {
                // Si es la primera vez, inicia un retardo
                firstShot = false;
                StartCoroutine(InitialShotDelay(left));
            }
            else
            {
                FireProjectile(left);
            }
        }
    }

    private void FireProjectile(bool left)
    {
        nextFireTime = Time.time + fireRate;

        // Cambia el sprite al de disparo
        spriteRenderer.sprite = shootingSprite;

        // Dispara el proyectil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.tag = "ProjectileEnemy";
        projectile.GetComponent<Projectile>().SetDirection(left);

        // Vuelve al sprite normal después de un tiempo
        Invoke("ResetSprite", shootDuration);
    }

    private IEnumerator InitialShotDelay(bool left)
    {
        yield return new WaitForSeconds(initialDelay);
        FireProjectile(left);
    }

    private void ResetSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
