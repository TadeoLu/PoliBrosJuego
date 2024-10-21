using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popup; // Panel del popup
    public InputField idInput; // Campo de entrada para el ID
    public Button mostrarPopupButton; // Botón que muestra el popup
    public Button jugarButton; // Botón de jugar dentro del popup

    void Start()
    {
        // Asegúrate de que el popup esté inactivo al inicio
        popup.SetActive(false);

        // Agrega un listener al botón que muestra el popup
        mostrarPopupButton.onClick.AddListener(ShowPopup);
        // Agrega un listener al botón de jugar dentro del popup
        jugarButton.onClick.AddListener(OnJugarButtonClicked);
    }

    // Método que muestra el popup
    void ShowPopup()
    {
        popup.SetActive(true);
        // Ocultar el botón que muestra el popup
        mostrarPopupButton.gameObject.SetActive(false);
    }

    // Método que se llama cuando se presiona el botón "Jugar" dentro del popup
    void OnJugarButtonClicked()
    {
        // Obtener el texto del InputField
        string id = idInput.text;
        
        // Verificar que el ID no esté vacío
        if (!string.IsNullOrEmpty(id))
        {
            // Almacena el ID en el DataManager
            DataManager.Instance.usuarioId = id;

            // Cargar la escena especificada
            SceneManager.LoadScene("crearMapa");
        }
        else
        {
            // Mensaje de error si el ID está vacío
            Debug.Log("Por favor, inserte un ID.");
        }
    }
}
