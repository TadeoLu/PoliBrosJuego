using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
