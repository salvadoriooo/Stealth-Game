using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] button;

    public void Awake ()
    {
        int unlockedLevel = PlayerPrefs.GetInt ("UnlockedLevel", 1);

        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            button[i].interactable = true;
        }
    }
    public void OpenLevel(int levelIndex)
    {
        string level = "Level" + levelIndex;
        SceneManager.LoadScene(level);
    }
}
