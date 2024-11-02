using UnityEngine;

public class EnemyWalker : Enemy
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    public Sprite fantasma1; 
    public Sprite fantasma2; 
    protected override void Update()
    {
        base.Update();
    }

    protected void MoveTowardsPlayer(Vector3 direction)
    {
        // Mueve el enemigo hacia el jugador
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    protected override void DetectPlayer()
    {
        // Verifica la distancia entre el enemigo y el jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            GetComponent<SpriteRenderer>().sprite = fantasma1;
            PerformAction();
        }else{
            GetComponent<SpriteRenderer>().sprite = fantasma2;
        }
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
