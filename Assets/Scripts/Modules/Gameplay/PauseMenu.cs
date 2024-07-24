using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsMenuUI;
    [SerializeField] GameObject CoinUI;
    [SerializeField] GameObject NotifyMessage;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;
    
    void Start ()
    {
        FinishPoint.OnReachedEndOfLevel += HideNotifyMessage;
    }

    void OnDestroy ()
    {
        // Отписываемся от события
        FinishPoint.OnReachedEndOfLevel -= HideNotifyMessage;
    }

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

    private void Awake ()
    {
        _resumeButton.onClick.AddListener (Resume);
        _optionsButton.onClick.AddListener (OpenOptions);
        _quitButton.onClick.AddListener (Quit);
    }

    public void Resume ()
    {
        CoinUI.SetActive (true);
        PauseMenuUI.SetActive (false);
        OptionsMenuUI.SetActive (false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause ()
    {
        CoinUI.SetActive (false);
        PauseMenuUI.SetActive (true);
        NotifyMessage.SetActive (false);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    private void OpenOptions ()
    {
        OptionsMenuUI.SetActive (true);
        PauseMenuUI.SetActive (false);
    }

    private void Quit ()
    {
        SceneManager.LoadScene (0);
        ResetGamePauseStatus ();
    }

    private void ResetGamePauseStatus ()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    private void HideNotifyMessage ()
    {
        NotifyMessage.SetActive (false);
    }
}
