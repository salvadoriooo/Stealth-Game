using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {

            SceneController.instance.NextLevel ();
            UnlockNewLevel ();
        }
    }
    void UnlockNewLevel ()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt ("ReachedIndex")){
        PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel", 1) + 1);
        PlayerPrefs.Save ();
        }
    }
}
