using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
        if (SceneManager.GetActiveScene().name == "historia" && DataManager.Instance.usuarioId == "877438172072")
        {
            DataManager.Instance.usuarioId = "882238491501";
            SceneManager.LoadScene("historia");
        }else
        {
            int muertesNube = PlayerPrefs.GetInt("Muertes", 1); // 1 como valor por defecto si no se encuentra
            StartCoroutine(callApi(muertesNube));   
            PlayerPrefs.SetInt("Muertes", 0);
            DataManager.Instance.usuarioId = "696798712375";
            SceneManager.LoadScene("ruleta");
        	Destroy(gameObject); 
        }
            
        }
    }

        private IEnumerator callApi(int muertes)
    {
        string apiUrl = "http://localhost:3000/api/mapas/muertes/" + DataManager.Instance.usuarioId;
        string jsonData = "{\"muertes\": " + muertes + "}";
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");

        // Set the request body to the JSON data
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        
        // Set the content type to "application/json"
        request.SetRequestHeader("Content-Type", "application/json");

        // Set up the download handler to handle the response
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request and wait for the response
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResult = request.downloadHandler.text;
	        Debug.Log("Response: " + jsonResult);
        }
    }
}



