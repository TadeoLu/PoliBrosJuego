using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ruleta : MonoBehaviour
{
    public float RotatePower;
    public float StopPower;
    public int jefe = 1; // 1 rojo, 2 amarillo, 3 verde
    private Rigidbody2D rbody;
    int inRotate;


    private void Start()
    {
        int randomNumber = GenerateRandomNumber(400, 2000);
        StopPower = randomNumber;
        rbody = GetComponent<Rigidbody2D>();
        Rotete();
    }

    float t;
    private void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= StopPower * Time.deltaTime;
            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();
                inRotate = 0;
                t = 0;
            }
        }
    }

    public void Rotete()
    {
        if (inRotate == 0)
        {
            rbody.AddTorque(RotatePower);
            inRotate = 1;
        }
    }

    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0 + 60 && rot <= 120 + 60)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 120);            
            jefe = 3;
            Win("verde");

        }
        else if (rot > 120 + 60 && rot <= 240 + 60)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 240);
            jefe = 2;
            Win("amarillo");
        }
        else // from 240 + 60 to 360 + 60
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
            jefe = 1;
            Win("rojo");
        }
    }

    public void Win(string Score)
    {
        print(Score);
        PlayerPrefs.SetInt("Jefe", jefe);
        PlayerPrefs.Save();
        // Inicia la corrutina con el retardo antes de cargar la escena
        StartCoroutine(DelayedSceneLoad());
    }

    // Corrutina para esperar un retardo antes de cargar la escena
    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(2f); // 2 segundos de retardo
        SceneManager.LoadScene("manual");
    }
        int GenerateRandomNumber(int min, int max)
    {
        return Random.Range(min, max + 1); // max + 1 para incluir 1000
    }
}
