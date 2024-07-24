using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
    }

    public void NextLevel ()
    {
        Debug.Log ("Загрузка следующего уровня");
        SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void LoadScene (string sceneName)
    {
        SceneManager.LoadSceneAsync (sceneName);
    }

    public void Message ()
    {
        Debug.Log ("Выводиться");
    }
}
