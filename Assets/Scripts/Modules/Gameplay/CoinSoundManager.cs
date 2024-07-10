using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundManager : MonoBehaviour
{
    public static CoinSoundManager instance;
    public AudioSource coinSource;
    public AudioClip coinCollected;
    private void Awake ()
    {
        if (instance == null)
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
