using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Navegator : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject[] buttons;
    private int currentIndex = 0; 
    public InputField inputField; // Asigna tu InputField en el inspector
    public float navigationDelay = 0.2f; // Tiempo de espera entre cada movimiento
    private bool canNavigate = true; // Controla si puede navegar o no
    private bool primeraVez = true;
    private bool canClick = true; // Controla si se puede hacer clic en el botón

    private void Start()
    {
        inputField.interactable = false; // Desactiva el InputField
        // Asegúrate de que el primer botón esté seleccionado al inicio
        eventSystem.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void Update()
    {
        if (primeraVez)
        {
            eventSystem.SetSelectedGameObject(buttons[0].gameObject);
            primeraVez = false;
        }
        // Navegación con flechas y control de delay
        if (canNavigate)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(NavigateWithDelay(-1));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(NavigateWithDelay(1));
            }
        }

        // Confirmar selección
        if (canClick && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O)))
        {
            StartCoroutine(ClickWithDelay());
        }
    }

    private IEnumerator NavigateWithDelay(int direction)
    {
        canNavigate = false; // Desactiva la navegación temporalmente
        Navigate(direction); // Llama a la función de navegación
        yield return new WaitForSeconds(navigationDelay); // Espera el tiempo especificado
        canNavigate = true; // Vuelve a activar la navegación
    }

 private IEnumerator ClickWithDelay()
{
    canClick = false; // Evita que se pueda hacer clic de inmediato
    yield return new WaitForSeconds(0.3f); // Espera 0.3 segundos antes de permitir el clic
    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    canClick = true; // Permite hacer clic de nuevo
}


    private void Navigate(int direction)
    {
        // Calcula el nuevo índice
        currentIndex += direction;

        // Asegúrate de que el índice se mantenga dentro de los límites
        if (currentIndex < 0)
        {
            currentIndex = buttons.Length - 1; // Va al último botón si es menor que 0
        }
        else if (currentIndex >= buttons.Length)
        {
            currentIndex = 0; // Regresa al primer botón si es mayor o igual al número de botones
        }

        eventSystem.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }
}
