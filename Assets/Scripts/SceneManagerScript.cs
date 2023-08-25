using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    void Awake()
    {
        instance = this;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
