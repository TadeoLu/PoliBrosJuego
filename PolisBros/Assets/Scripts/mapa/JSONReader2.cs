using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class JSONReader2 : MonoBehaviour
{
    // Nombre del archivo JSON
    private string jsonFileName = "grid(5)";

    // Referencia a los cuadrados en la fila
    public GameObject[] squares;
    
    // Textura para asignar cuando el nombre es "Image 3"
    public Texture2D bloque1; // Asigna la textura desde el Inspector
    public Texture2D bloque2; 
    
void Start()
{
    // Cargar el archivo JSON desde Resources
    TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

    if (jsonFile != null)
    {
        // Leer el contenido del JSON
        string jsonContent = jsonFile.text;

        // Usar expresiones regulares para encontrar todos los arrays dentro del JSON
        string pattern = @"\[([^\[\]]*)\]";
        MatchCollection matches = Regex.Matches(jsonContent, pattern);

        // Verificar que haya al menos dos arrays
        if (matches.Count >= 16)
        {
            // El segundo match contiene el segundo array
            string content = matches[1].Groups[1].Value;
            Debug.Log("Contenido del segundo array:");

            // Dividir el contenido en elementos, considerando las comas dentro de los objetos JSON
            List<string> items = SplitJsonArray(content);

            // Verificar si el número de elementos coincide con la cantidad de cuadrados
            if (items.Count == squares.Length)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string item = items[i];

                    if (item.Trim() == "null")
                    {
                        Debug.Log("null");
                    }
                    else if (item.Contains("{") && item.Contains("}"))
                    {
                        // Extraer el campo "name" del objeto JSON
                        string name = ExtractName(item);
                        Debug.Log("Name: " + name);

                        // Cambiar el color del cuadrado si el nombre es "Image 3"
                        if (name == "Image 3")
                        {
                            AddTextureToSquare(squares[i], bloque1);
                            //ChangeSquareColor(squares[i], Color.green);
                            AddBoxCollider(squares[i]);
                        }
                        else if (name == "Image 1")
                        {
                            AddTextureToSquare(squares[i], bloque2);
                            //ChangeSquareColor(squares[i], Color.red);
                            AddBoxCollider(squares[i]);
                        }
                    }
                    else
                    {
                        Debug.Log("Desconocido: " + item);
                    }
                }
            }
            else
            {
                Debug.LogError("El número de elementos en el segundo array no coincide con la cantidad de cuadrados.");
            }
        }
        else
        {
            Debug.LogError("No se encontraron suficientes arrays en el archivo JSON.");
        }
    }
    else
    {
        Debug.LogError("No se pudo encontrar el archivo JSON: " + jsonFileName);
    }
}


    // Método para dividir el array JSON considerando los objetos que contienen comas
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

    // Método para extraer el campo "name" de un objeto JSON
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

    // Método para cambiar el color de un cuadrado
    void ChangeSquareColor(GameObject square, Color color)
    {
        if (square != null)
        {
            var renderer = square.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
    void AddBoxCollider(GameObject square)
    {
        if (square != null)
        {
            var collider = square.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                square.AddComponent<BoxCollider2D>();
            }
        }
    }
    void AddTextureToSquare(GameObject square, Texture2D texture)
    {
        if (square != null && texture != null)
        {
 // Obtener el componente SpriteRenderer del GameObject square
        SpriteRenderer spriteRenderer = square.GetComponent<SpriteRenderer>();

        // Verificar si se encontró el componente SpriteRenderer
        if (spriteRenderer != null)
        {
            // Crear un nuevo sprite con la textura proporcionada
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Asignar el nuevo sprite al SpriteRenderer
            spriteRenderer.sprite = sprite;

            square.transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else
        {
            Debug.LogError("El GameObject no tiene un componente SpriteRenderer.");
        }
        }

    }
}
