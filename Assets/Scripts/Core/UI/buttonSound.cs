using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] public AudioClip soundHover;
    [SerializeField] public AudioClip soundClick;
    private AudioSource _audioSource;
    

    private void Awake ()
    {
        _audioSource = this.GetComponent<AudioSource> ();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        _audioSource.PlayOneShot (soundHover);
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        _audioSource.PlayOneShot (soundClick);
    }
}
