using UnityEngine;

public class EnemyWalker : Enemy
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    protected override void Update()
    {
        base.Update();
    }

    protected void MoveTowardsPlayer(Vector3 direction)
    {
        // Mueve el enemigo hacia el jugador
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    protected override void PerformAction()
    {
        // Determina la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Mueve el enemigo hacia el jugador
        MoveTowardsPlayer(direction);

        // Calcula el giro
        if (direction.x > 0)
        {
            Flip(true); // Gira a la derecha
        }
        else
        {
            Flip(false); // Gira a la izquierda
        }
    }
}
