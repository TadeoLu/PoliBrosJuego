using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public InputField idInput; // Campo de entrada para el ID
    public Button jugarButton; // Botón de jugar dentro del popup

    void Start()
    {
        idInput.text = GUIUtility.systemCopyBuffer;
        // Agrega un listener al botón de jugar dentro del popup
        jugarButton.onClick.AddListener(OnJugarButtonClicked);
    }

    // Método que se llama cuando se presiona el botón "Jugar" dentro del popup
    public void OnJugarButtonClicked()
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
