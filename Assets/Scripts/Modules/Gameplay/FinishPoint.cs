

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
        Debug.Log ("Przejście na kolejny poziom");
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





