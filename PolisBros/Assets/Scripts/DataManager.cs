using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string usuarioId; // Almacena el ID de usuario

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Asegúrate de que este objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados
        }
    }
}
