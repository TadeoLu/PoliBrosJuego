using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            DataManager.Instance.usuarioId = "696798712375";
            SceneManager.LoadScene("ruleta");
        	Destroy(gameObject); 
        }
            
        }
    }
}



