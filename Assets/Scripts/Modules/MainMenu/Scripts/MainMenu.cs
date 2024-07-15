using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _exitButton;
    public void PlayGame ()
    {
        SceneManager.LoadSceneAsync (1);
    }

    public void ExitGame ()
    {
        Application.Quit ();
    }
}
