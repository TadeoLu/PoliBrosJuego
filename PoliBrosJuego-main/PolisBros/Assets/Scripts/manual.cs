using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manual : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite pruscino;
    public Sprite barbieri;
    public Sprite marinelli;
    void Start()
    {
        int jefe = PlayerPrefs.GetInt("Jefe", 1); // 1 como valor por defecto si no se encuentra
        Debug.Log("Valor de jefe: " + jefe);
        switch(jefe){
            case 2:
                GetComponent<SpriteRenderer>().sprite = pruscino;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = barbieri;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = marinelli;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
