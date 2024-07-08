using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int totalCoinsCollected = 0;
    public TextMeshProUGUI textMeshPro;

    private void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            totalCoinsCollected++;
            textMeshPro.text = totalCoinsCollected.ToString();
            Debug.Log (totalCoinsCollected);
            Soundmanager.instance.coinSource.PlayOneShot (Soundmanager.instance.coinCollected);
            Destroy (other.gameObject);
        }
    }
}
