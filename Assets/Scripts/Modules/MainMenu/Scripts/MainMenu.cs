using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private OptionsMenu _optionsMenu;
    [SerializeField] private LevelMenu _levelMenu;
    [SerializeField] private GameObject _mainMenuView;

    private void Awake ()
    {
        _playButton.onClick.AddListener (OpenLevelMenu);
        _optionsButton.onClick.AddListener (OpenOptions);
    }
    private void OpenLevelMenu ()
    {
        _mainMenuView.gameObject.SetActive (false);
        _levelMenu.gameObject.SetActive (true);
    }

    private void OpenOptions ()
    {
        _optionsMenu.gameObject.SetActive (true);
        _mainMenuView.SetActive (false);
    }

    private void ExitGame ()
    {
        Application.Quit ();
    }
}
