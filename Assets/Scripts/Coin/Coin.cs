using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speedRotate;
   

    void Update()
    {
        transform.Rotate(Vector3.up * speedRotate * Time.deltaTime, Space.World);
    }

    
}
