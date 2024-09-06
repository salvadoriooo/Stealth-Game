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

    // Metoda wywoływana przy uruchomieniu skryptu, inicjalizuje przyciski
    private void Awake ()
    {
        // Powiązanie przycisku "Play" z metodą otwierającą menu wyboru poziomu
        _playButton.onClick.AddListener (OpenLevelMenu);
        // Powiązanie przycisku "Settings" z metodą otwierającą menu ustawień
        _optionsButton.onClick.AddListener (OpenOptions);
        _exitButton.onClick.AddListener(ExitGame);
    }

    // Metoda wywoływana po kliknięciu przycisku "Play", ukrywa główne menu i wyświetla menu wyboru poziomu
    private void OpenLevelMenu ()
    {
        _mainMenuView.gameObject.SetActive (false);// Ukrycie głównego menu
        _levelMenu.gameObject.SetActive (true);// Wyświetlenie menu wyboru poziomu
    }

    // Metoda wywoływana po kliknięciu przycisku "Settings", ukrywa główne menu i wyświetla menu ustawień
    private void OpenOptions ()
    {
        _optionsMenu.gameObject.SetActive (true);// Wyświetlenie menu ustawień
        _mainMenuView.SetActive (false);// Ukrycie głównego menu
    }

    // Metoda wywoływana po kliknięciu przycisku "Exit", zamyka aplikację
    private void ExitGame ()
    {
        Application.Quit (); // Zamyka grę/aplikację
    }
}
