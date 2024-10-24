using UnityEngine;

public class EnemyJefe : Enemy
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float fireRate = 1f; // Tasa de disparo
    private float nextFireTime = 0f; // Tiempo para el próximo disparo
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
        protected void verificarVidas(){
        // if (vidas<vidas/2)
        // {
        //     Laser laserScript = new Laser();
        //     StartCoroutine(laserScript.SpawnLaserLoop());
        // }
        if(vidas <= 0){
            Destroy(gameObject);
        }
    }
}
