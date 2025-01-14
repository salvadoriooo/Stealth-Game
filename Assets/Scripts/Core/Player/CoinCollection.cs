


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectedCoinsText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    private int totalCoinsCollected = 0;
    private int totalCoinsInLevel;
    private FinishPoint finishPoint;

    public bool AllCoinsCollected => totalCoinsCollected >= totalCoinsInLevel;

    private void Start ()
    {
        Initialize ();
    }

    private void Initialize ()
    {
        finishPoint = FindObjectOfType<FinishPoint> ();
        totalCoinsInLevel = GameObject.FindGameObjectsWithTag ("Coin").Length;
        UpdateTotalCoinsText ();
        UpdateCollectedCoinsText ();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Coin"))
        {
            CollectCoin (other);
        }
    }

    private void CollectCoin (Collider coin)
    {
        totalCoinsCollected++;
        UpdateCollectedCoinsText ();
        PlayCoinSound ();
        Destroy (coin.gameObject);

        if (totalCoinsCollected == totalCoinsInLevel)
        {
            finishPoint.SetAllCoinsCollected (true);
        }
    }

    private void UpdateCollectedCoinsText ()
    {
        collectedCoinsText.text = totalCoinsCollected.ToString ();
        Debug.Log (totalCoinsCollected);
    }

    private void UpdateTotalCoinsText ()
    {
        totalCoinsText.text = "/ " + totalCoinsInLevel.ToString ();
    }

    private void PlayCoinSound ()
    {
        CoinSoundManager.instance.coinSource.PlayOneShot (CoinSoundManager.instance.coinCollected);
    }
}


