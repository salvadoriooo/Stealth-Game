using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;
    [SerializeField] Button _buttonBack;
    [SerializeField] GameObject _previousView;
    private void Awake ()
    {
        slider.onValueChanged.AddListener(SetVolume);
        toggle.onValueChanged.AddListener (SetFullScreen);
        _buttonBack.onClick.AddListener (BackToMenu);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat ("volume", volume);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void BackToMenu ()
    {
        gameObject.SetActive  (false);
        _previousView.SetActive (true);

    }
}
