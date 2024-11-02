	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class JSONReader : MonoBehaviour
{
    private string jsonFileName = "grid";
    public GameObject[] squares;
    public Texture2D bloque1; 
    public Texture2D bloque2;       
    public Texture2D arena;
    public Texture2D blanco;
    public Texture2D ladrillo;
    public Texture2D maderaClara;
    public Texture2D maderaOscura;
    public Texture2D salas;
    public Texture2D piedraFea;
    public Texture2D piedraLinda;
    public Texture2D pisoCity;
    public Texture2D ventana;
    public PhysicsMaterial2D physicsMaterial2D;
    public GameObject prefab; 
    public GameObject prefab2; 
    public GameObject spawnPrefab; // Prefab para el spawn point
    public GameObject finishPrefab; // Prefab para el finish point
    public GameObject prefabShooter;
    public GameObject prefabWalker;
    public EventSystem eventSystem;
    private Transform playerTransform;
    private List<GameObject> enemiesNotAssigned = new List<GameObject>();


    void Start()
    {
        StartCoroutine(pana());
    }

    IEnumerator pana()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        ApiManager api = new ApiManager();
        yield return StartCoroutine(api.GetUsuarios(DataManager.Instance.usuarioId));
        //yield return StartCoroutine(api.GetUsuarios("799644323705"));
        string mapa = api.mapa;
        Debug.Log("Mapa: " + mapa);
        
        if (mapa != null)
        {
            string jsonContent = mapa;
            string pattern = @"\[([^\[\]]*)\]";
            MatchCollection matches = Regex.Matches(jsonContent, pattern);
            List<string> allItems = new List<string>();

            foreach (Match match in matches)
            {
                string content = match.Groups[1].Value;
                List<string> items = SplitJsonArray(content);
                allItems.AddRange(items);
            }

            Debug.Log("Número total de elementos coincide con la cantidad de cuadrados.");

            for (int i = 0; i < allItems.Count; i++)
            {
                string item = allItems[i].Trim();
                if (item == "null")
                {
                    //null
                }
                else if (item.Contains("{") && item.Contains("}"))
                {
                    
                    string name = ExtractName(item);
                    if (name == "TierraPasto")
                    {
                        AddTextureToSquare(squares[i], bloque1);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "Tierra")
                    {
                        AddTextureToSquare(squares[i], bloque2);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "Arena")
                    {
                        AddTextureToSquare(squares[i], arena);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "Ladrillo")
                    {
                        AddTextureToSquare(squares[i], ladrillo);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "MaderaClara")
                    {
                        AddTextureToSquare(squares[i], maderaClara);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "MaderaOscura")
                    {
                        AddTextureToSquare(squares[i], maderaOscura);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "Salas")
                    {
                        AddTextureToSquare(squares[i], salas);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "PiedraFea")
                    {
                        AddTextureToSquare(squares[i], piedraFea);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "PiedraLinda")
                    {
                        AddTextureToSquare(squares[i], piedraLinda);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "PisoCity")
                    {
                        AddTextureToSquare(squares[i], pisoCity);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "Ventana")
                    {
                        AddTextureToSquare(squares[i], ventana);
                        AddBoxCollider(squares[i]);
                    }
                    else if (name == "EnemyWalker")
                    {
                        InstantiateEnemyPrefabAtSquare(prefabWalker, squares[i]);
                    }
                    else if (name == "EnemyShooter")
                    {
                        InstantiateEnemyPrefabAtSquare(prefabShooter, squares[i]);
                    }
                    else if (name == "Gonza") // Spawn point
                    {
                        GameObject playerObject = Instantiate(spawnPrefab, squares[i].transform.position, Quaternion.identity);

                        playerMovement playerScript = playerObject.GetComponent<playerMovement>();
                        if (playerScript != null)
                        {
                            playerScript.spawn = squares[i].transform.position; // Set spawn point
                            playerScript.eventSystem = eventSystem;
                            if (SceneManager.GetActiveScene().name == "jefe"){
                                playerScript.vidas = 3; 
                            }
                        }

                        playerTransform = playerObject.transform;
                        CameraScript cameraScript = Camera.main.GetComponent<CameraScript>();
                        if (cameraScript != null)
                        {
                            cameraScript.John = playerTransform;
                        }
                        foreach(GameObject enemy in enemiesNotAssigned){
                            Enemy enemyScript = enemy.GetComponent<Enemy>();
                            if (enemyScript != null)
                            {
                                enemyScript.player = playerTransform;
                            }
                        }
                    }
                    else if (name == "Finish") // Finish point
                    {
                        Instantiate(finishPrefab, squares[i].transform.position + new Vector3(0, 0.075f, 0), Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No se pudo encontrar el archivo JSON: " + mapa);
        }
    }

    List<string> SplitJsonArray(string jsonArray)
    {
        var items = new List<string>();
        var stack = new Stack<char>();
        var item = "";

        foreach (var c in jsonArray)
        {
            if (c == ',' && stack.Count == 0)
            {
                items.Add(item.Trim());
                item = "";
            }
            else
            {
                item += c;
                if (c == '{') stack.Push(c);
                if (c == '}') stack.Pop();
            }
        }

        if (item.Length > 0)
        {
            items.Add(item.Trim());
        }

        return items;
    }

    string ExtractName(string jsonObject)
    {
        var pattern = @"""name""\s*:\s*""(.*?)""";
        var match = Regex.Match(jsonObject, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return "undefined";
    }

    void AddBoxCollider(GameObject square)
    {

        if (square != null)
        {
            // Obtén o añade el BoxCollider2D
            var collider = square.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = square.AddComponent<BoxCollider2D>();
            }

            // Asigna el Physics Material 2D al BoxCollider2D
            if (physicsMaterial2D != null)
            {
                collider.sharedMaterial = physicsMaterial2D;
            }
            else
            {
                Debug.LogWarning("El Physics Material 2D no está asignado.");
            }        
            }
    }

    void AddTextureToSquare(GameObject square, Texture2D texture)
    {
        if (square != null && texture != null)
        {
            SpriteRenderer spriteRenderer = square.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                spriteRenderer.sprite = sprite;

                square.transform.localScale = new Vector3(3f, 3f, 1f); // Ajusta el tamaño según sea necesario
            }
            else
            {
                //Debug.LogError("El GameObject no tiene un componente SpriteRenderer.");
            }
        }
    }
        void InstantiatePrefabAtSquare(GameObject prefab, GameObject square)
    {
        if (square != null && prefab != null)
        {
            Instantiate(prefab, square.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab no asignado o el cuadrado es nulo.");
        }
    }
    
    void InstantiateEnemyPrefabAtSquare(GameObject prefabShooter, GameObject square){
	GameObject enemyObject = Instantiate(prefabShooter, square.transform.position, Quaternion.identity);
	Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            if(playerTransform != null){
                enemyScript.player = playerTransform; // Asigna el transform del jugador
            }else{
                enemiesNotAssigned.Add(enemyObject);
            }
        }
    }
}

