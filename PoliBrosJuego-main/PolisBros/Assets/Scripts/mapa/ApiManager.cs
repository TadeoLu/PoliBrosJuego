using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ApiManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:3000/api/mapas/";
    public string mapa = "";

    void Start()
    {
    }

    public IEnumerator GetUsuarios(string id)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + id);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Obtener la respuesta como texto
            string jsonResult = request.downloadHandler.text;
	    Debug.Log("Response: " + jsonResult);
            // Deserializar la respuesta para obtener el email y valores
            //List<MapaData> mapas = JsonUtility.FromJson<Wrapper<MapaData>>(WrapArray(jsonResult)).mapas;
	    MapaData mapaData = JsonUtility.FromJson<MapaData>(jsonResult);
            
            this.mapa = mapaData.valores;
            Debug.Log("Mapa:" + this.mapa);
            }
        }
    

    // Función auxiliar para envolver el JSON en un objeto
    private string WrapArray(string jsonArray)
    {
        return "{\"mapas\":" + jsonArray + "}";
    }
}

[System.Serializable]
public class MapaData
{
    public CreatorData creator;
    public string valores;
    public string _id;
    public long id;
    public string name;
}

[System.Serializable]
public class CreatorData
{
    public long id;
    public string username;
    public string email;
    public string password;
}

// Clase envolvente para deserializar el array JSON
[System.Serializable]
public class Wrapper<T>
{
    public List<T> mapas;
}

