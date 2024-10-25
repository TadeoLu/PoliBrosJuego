using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab del cuadrado láser
    public Vector2 xRange = new Vector2(-1.24f, 0.8f); // Rango de aparición en el eje X
    public Color initialColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Color gris claro
    public Color alertColor = Color.red; // Color rojo
    private GameObject laserInstance;
    public bool laserActivo = false;    

    public IEnumerator SpawnLaser()
    {
        if(laserInstance != null){
            laserInstance.GetComponent<Laser>().laserActivo = false;
        }
        // Posición aleatoria dentro del rango de x
        float randomX = Random.Range(xRange.x, xRange.y);
        Vector3 spawnPosition = new Vector3(randomX, 0, 0); // Cambia Y y Z si es necesario

        // Instanciar el láser (cuadrado)
        laserInstance = Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
        // Cambiar el color inicial a gris claro
        Renderer laserRenderer = laserInstance.GetComponent<Renderer>();
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = alertColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = alertColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = alertColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = alertColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = alertColor;
        yield return new WaitForSeconds(0.1f);
        laserRenderer.material.color = initialColor;
        yield return new WaitForSeconds(0.1f);

        laserInstance.GetComponent<Laser>().laserActivo =true;
        // Cambiar el color a rojo
        laserRenderer.material.color = alertColor;
        
        // Esperar 9 segundos más (para que en total sean 10 segundos entre apariciones)
        yield return new WaitForSeconds(4f);

        Destroy(laserInstance);
    }

}
