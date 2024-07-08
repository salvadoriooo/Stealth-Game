using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
        int _totalCoin = coins.Length;
       textMeshProUGUI.text = "/ " + _totalCoin.ToString ();
    }

  
   
}
