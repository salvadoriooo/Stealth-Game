using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager instance;
    public AudioSource coinSource;
    public AudioClip coinCollected;
    private void Awake ()
    {
        instance = this;
    }
    void Start()
    {
        coinSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
