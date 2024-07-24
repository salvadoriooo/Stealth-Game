//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class FinishPoint : MonoBehaviour
//{

//    [SerializeField] TextMeshProUGUI _notifyMessage;

//    private bool _allCoinsCollected = false;
//    private void OnTriggerEnter (Collider other)
//    {
//        if (other.CompareTag ("Player"))
//        {
//            if (_allCoinsCollected)
//            {
//                SceneController.instance.NextLevel ();
//                UnlockNewLevel ();
//            }

//        }
//        else
//        {
//            ShowMessage ("Zbierz wszystkie monety, aby ukończyć poziom!");
//        }
//    }
//    void UnlockNewLevel ()
//    {
//        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt ("ReachedIndex")){
//        PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
//        PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel", 1) + 1);
//        PlayerPrefs.Save ();
//        }
//    }

//    public void SetAllCoinsCollected (bool status)
//    {
//        _allCoinsCollected = status;

//        if (status)
//        {
//            ShowMessage ("Wszystkie monety zerbane! Idź do wyjsćia");
//        }
//    }

//    void ShowMessage(string message)
//    {
//        _notifyMessage.text = message;

//        _notifyMessage.text = message;

//        _notifyMessage.gameObject.SetActive (true);
//    }
//}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class FinishPoint : MonoBehaviour
//{
//    [SerializeField] private TextMeshProUGUI messageText;
//    private bool allCoinsCollected = false;

//    private void OnTriggerEnter (Collider other)
//    {
//        if (other.CompareTag ("Player"))
//        {
//            if (allCoinsCollected)
//            {
//                ProceedToNextLevel ();
//            }
//            else
//            {
//                ShowMessage ("Zbierz wszystkie monety, aby ukończyć poziom!");
//            }
//        }
//    }

//    private void ProceedToNextLevel ()
//    {
//        SceneController.instance.NextLevel ();
//        UnlockNewLevel ();
//    }

//    private void UnlockNewLevel ()
//    {
//        int currentLevelIndex = SceneManager.GetActiveScene ().buildIndex;
//        if (currentLevelIndex >= PlayerPrefs.GetInt ("ReachedIndex"))
//        {
//            PlayerPrefs.SetInt ("ReachedIndex", currentLevelIndex + 1);
//            PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel", 1) + 1);
//            PlayerPrefs.Save ();
//        }
//    }

//    public void SetAllCoinsCollected (bool status)
//    {
//        allCoinsCollected = status;
//        if (status)
//        {
//            ShowMessage ("Wszystkie monety zebrane! Idź do wyjścia.");
//        }
//    }

//    private void ShowMessage (string message)
//    {
//        messageText.text = message;
//        messageText.gameObject.SetActive (true);
//    }
//}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class FinishPoint : MonoBehaviour
//{
//    [SerializeField] private TextMeshProUGUI messageText;
//    private bool allCoinsCollected = false;
//    public static event System.Action OnReachedEndOfLevel;


//    private void Awake ()
//    {
//        HideMessage ();
//    }
//    private void OnTriggerEnter (Collider other)
//    {
//        if (other.CompareTag ("Player"))
//        {
//            if (allCoinsCollected)
//            {
//                OnReachedEndOfLevel?.Invoke ();
//                ProceedToNextLevel ();
//            }
//            else
//            {
//                ShowMessage ("Zbierz wszystkie monety, aby ukończyć poziom!");
//            }
//        }
//    }

//    private void OnTriggerExit (Collider other)
//    {
//        if (other.CompareTag ("Player") && !allCoinsCollected)
//        {
//            HideMessage ();
//        }
//    }

//    private void ProceedToNextLevel ()
//    {
//        ShowMessage ("Wszystkie monety zebrane! Idź do wyjścia.");
//        SceneController.instance.NextLevel ();
//        UnlockNewLevel ();
//    }

//    private void UnlockNewLevel ()
//    {
//        int currentLevelIndex = SceneManager.GetActiveScene ().buildIndex;
//        if (currentLevelIndex >= PlayerPrefs.GetInt ("ReachedIndex"))
//        {
//            PlayerPrefs.SetInt ("ReachedIndex", currentLevelIndex + 1);
//            PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel", 1) + 1);
//            PlayerPrefs.Save ();
//        }
//    }

//    public void SetAllCoinsCollected (bool status)
//    {
//        allCoinsCollected = status;
//        if (status)
//        {
//            ShowMessage ("Wszystkie monety zebrane! Idź do wyjścia.");
//        }
//    }

//    public void ShowMessage (string message)
//    {
//        messageText.text = message;
//        messageText.gameObject.SetActive (true);
//    }

//    private void HideMessage ()
//    {
//        messageText.gameObject.SetActive (false);
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    private bool allCoinsCollected = false;
    public static event System.Action OnReachedEndOfLevel;

    void Awake ()
    {
        HideMessage ();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            if (allCoinsCollected)
            {
                OnReachedEndOfLevel?.Invoke ();
            }
            else
            {
                ShowMessage ("Zbierz wszystkie monety, aby ukończyć poziom!");
            }
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            HideMessage ();
        }
    }

    public void ProceedToNextLevel ()
    {
        Debug.Log ("Переход на следующий уровень");
        SceneController.instance.NextLevel ();
        UnlockNewLevel ();
    }

    private void UnlockNewLevel ()
    {
        int currentLevelIndex = SceneManager.GetActiveScene ().buildIndex;
        if (currentLevelIndex >= PlayerPrefs.GetInt ("ReachedIndex"))
        {
            PlayerPrefs.SetInt ("ReachedIndex", currentLevelIndex + 1);
            PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save ();
        }
    }

    public void SetAllCoinsCollected (bool status)
    {
        allCoinsCollected = status;
        if (status)
        {
            ShowMessage ("Wszystkie monety zebrane! Idź do wyjścia.");
        }
    }

    public void ShowMessage (string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive (true);
    }

    private void HideMessage ()
    {
        messageText.gameObject.SetActive (false);
    }

    public bool AreAllCoinsCollected ()
    {
        return allCoinsCollected;
    }
}





