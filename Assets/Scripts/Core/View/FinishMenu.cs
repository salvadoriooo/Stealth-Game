
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;
//public class FinishUI : MonoBehaviour
//{
//    public GameObject _gameLoseUI;
//    public GameObject _gameWinUI;
//    public GameObject _finishPointUI;
//    bool _gameIsOver;
//    void Start()
//    {
//        GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");


//        _gameLoseUI.SetActive(false);
//        _gameWinUI.SetActive(false);
//        Guard.OnGuardHasSpottedPlayer += showGameLoseUI;
//        StationaryGuard.OnGuardHasSpottedPlayer += showGameLoseUI;
//        FindObjectOfType<Player>().OnReachedEndOfLevel += showGameWinUI;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (_gameIsOver)
//        {
//            _finishPointUI.SetActive(true);

//        }
//    }

//    void showGameWinUI()
//    {
//        OnGameOver(_gameWinUI);
//    }
//    void showGameLoseUI()
//    {
//        OnGameOver(_gameLoseUI);

//    }
//    void OnGameOver(GameObject gameOverUI)
//    {
//        gameOverUI.SetActive(true);
//        _gameIsOver = true;
//        Guard.OnGuardHasSpottedPlayer -= showGameLoseUI;
//        StationaryGuard.OnGuardHasSpottedPlayer -= showGameLoseUI;
//        FindObjectOfType<Player>().OnReachedEndOfLevel -= showGameWinUI;
//    }

//    public void OpenLevel (int levelIndex)
//    {
//        string level = "Level" + levelIndex;
//        SceneManager.LoadScene (level);
//    }

//}


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishUI : MonoBehaviour
{
    public GameObject _gameLoseUI;
    public GameObject _gameWinUI;
    public GameObject _finishPointUI;
    private bool _gameIsOver;
    private CoinCollection coinCollection;

    void Start ()
    {
        coinCollection = FindObjectOfType<CoinCollection> ();

        _gameLoseUI.SetActive (false);
        _gameWinUI.SetActive (false);
        Guard.OnGuardHasSpottedPlayer += showGameLoseUI;
        StationaryGuard.OnGuardHasSpottedPlayer += showGameLoseUI;
        FinishPoint.OnReachedEndOfLevel += TryShowGameWinUI;
    }

    void Update ()
    {
        if (_gameIsOver)
        {
            _finishPointUI.SetActive (true);
        }
    }

    private void TryShowGameWinUI ()
    {
        // Sprawdza, czy wszystkie monety zostały zebrane
        if (coinCollection.AllCoinsCollected)
        {
            showGameWinUI ();
        }
    }

    void showGameWinUI ()
    {
        OnGameOver (_gameWinUI);
    }

    void showGameLoseUI ()
    {
        OnGameOver (_gameLoseUI);
    }

    void OnGameOver (GameObject gameOverUI)
    {
        gameOverUI.SetActive (true);
        _gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= showGameLoseUI;
        StationaryGuard.OnGuardHasSpottedPlayer -= showGameLoseUI;
        FinishPoint.OnReachedEndOfLevel -= TryShowGameWinUI;
    }

    public void OpenLevel (int levelIndex)
    {
        string level = "Level" + levelIndex;
        SceneManager.LoadScene (level);
    }
}



