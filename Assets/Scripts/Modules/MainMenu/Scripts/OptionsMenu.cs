using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer; // Zmienna do manipulacji poziomem dźwięku
    [SerializeField] Slider slider;// Suwak do regulacji głośności
    [SerializeField] Toggle toggle;// Checkbox do przełączania trybu pełnoekranowego
    [SerializeField] Button _buttonBack;// Przycisk do powrotu do poprzedniego menu
    [SerializeField] GameObject _previousView;// Obiekt reprezentujący poprzednie menu
    private void Awake ()
    {
        slider.onValueChanged.AddListener(SetVolume);// Dodanie słuchacza na zmiany wartości suwka
        toggle.onValueChanged.AddListener (SetFullScreen); // Dodanie słuchacza na zmiany wartości checkboxa
        _buttonBack.onClick.AddListener (BackToMenu); // Dodanie słuchacza na kliknięcie przycisku powrotu
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat ("volume", volume); // Ustawienie poziomu dźwięku w mikserze audio
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen; // Ustawienie trybu pełnoekranowego lub okienkowego
    }

    public void BackToMenu ()
    {
        gameObject.SetActive  (false); // Ukrycie bieżącego widoku
        _previousView.SetActive (true); // Powrót do poprzedniego widoku

    }
}
