using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelButtons; // Tablica przycisk?w do wyboru poziomu

    public void Awake ()
    {
        int unlockedLevel = PlayerPrefs.GetInt ("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i+1 > unlockedLevel)
            {
                levelButtons[i].interactable = false;
            }
            
        }
        //for (int i = 0; i < unlockedLevel; i++)
        //{
        //    levelButtons[i].interactable = true;
        //}
    }
    public void OpenLevel(int levelIndex)
    {
        string level = "Level" + levelIndex;
        SceneManager.LoadScene(level);
    }
}
