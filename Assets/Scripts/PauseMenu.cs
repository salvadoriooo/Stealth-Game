using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject CoinUI;
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume ();
            }
            else
            {
                Pause ();
            }
        }
    }

    public void Resume ()
    {
        CoinUI.SetActive (true);
        PauseMenuUI.SetActive (false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause ()
    {
        CoinUI.SetActive (false);
        PauseMenuUI.SetActive (true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    
    public void Quit ()
    {
        SceneManager.LoadScene (0);
        ResetGamePauseStatus ();
    }

    void ResetGamePauseStatus ()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
}
