
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
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    public GameObject _gameLoseUI;
    public GameObject _gameWinUI;
    public GameObject _finishPointUI;
    private bool _gameIsOver;
    private CoinCollection coinCollection;

    [SerializeField] private Button nextLevelButton;

    void Start ()
    {
        coinCollection = FindObjectOfType<CoinCollection> ();

        _gameLoseUI.SetActive (false);
        _gameWinUI.SetActive (false);

        Guard.OnGuardHasSpottedPlayer += showGameLoseUI;
        StationaryGuard.OnGuardHasSpottedPlayer += showGameLoseUI;

        FinishPoint.OnReachedEndOfLevel += TryShowGameWinUI;


        // Add listener for the Next Level button
       
    }

    void Awake ()
    {
        nextLevelButton.onClick.AddListener (OnNextLevelButtonClicked);
        Debug.Log ("Назначение слушателя для кнопки 'Next Level'");
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

    public void OnNextLevelButtonClicked ()
    {
        Debug.Log ("Кнопка нажата");
        FinishPoint finishPoint = FindObjectOfType<FinishPoint> ();
        if (finishPoint != null)
        {
            Debug.Log ("FinishPoint найден");
            finishPoint.ProceedToNextLevel ();
        }
        else
        {
            Debug.Log ("FinishPoint не найден");
        }
    }
}



